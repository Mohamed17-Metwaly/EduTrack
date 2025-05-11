using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.DTO;
using EduTrack.Models;
using EduTrack.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.DataAccess
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly EduTrackContext _context;
        public StudentRepository(EduTrackContext context) : base(context)
        {
            _context = context;
        }
        public async Task UpdateAsync(Student student)
        {
            var existingStudent = await _context.Students.FindAsync(student.Id);
            if (existingStudent != null)
            {
                _context.Entry(existingStudent).CurrentValues.SetValues(student);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<CourseDTO>> GetCoursesAsync(int studentId)
        {
            var student = await _context.Students
                                  .Include(s => s.Enrollments)
                                      .ThenInclude(e => e.Course)
                                  .FirstOrDefaultAsync(s => s.Id == studentId);
            var courses = student.Enrollments.Select(e => e.Course).ToList();
            var list = new List<CourseDTO>();
            list.AddRange(courses.Select(c => new CourseDTO
            {
                Name = c.Name,
                code = c.code,
                CreditHours = c.CreditHours
            }));
            return list;
        }
        public async Task<List<CourseDTO>> GetAvailableCourses(int studentId)
        {
            var registeredCourseIds = await _context.Enrollments
                                              .Where(e => e.StudentId == studentId)
                                              .Select(e => e.CourseId)
                                              .ToListAsync();
            var availableCourses =await _context.Courses
                .Where(c => !registeredCourseIds.Contains(c.Id))
                .Select(c => new CourseDTO
                {
                   code= c.code,
                   Name= c.Name,
                   CreditHours= c.CreditHours
                })
                .ToListAsync();
            return availableCourses;
        }
        public async Task CourseRegistration(CourseRegistrationDTO registration)
        {
            // نجيب الـ Student الأول ونتأكد إنه موجود (اختياري)
            var studentExists = await _context.Students.AnyAsync(s => s.Id == registration.StudentId);
            if (!studentExists)
                return ;//not found student

            // نتأكد إن المواد صحيحة
            var validCourseIds = await _context.Courses
                .Where(c => registration.CourseIds.Contains(c.Id))
                .Select(c => c.Id)
                .ToListAsync();
            var latestSemester = await _context.semesters
                                          .OrderByDescending(s => s.Id) // أو OrderByDescending(s => s.StartDate) لو عندك تاريخ بداية
                                          .FirstOrDefaultAsync();

            if (latestSemester == null)
            {
                throw new Exception("لا يوجد ترم دراسي متاح حالياً.");
            }

            // نسجّل كل مادة
            foreach (var courseId in validCourseIds)
            {
                var alreadyEnrolled = await _context.Enrollments
                    .AnyAsync(e => e.StudentId == registration.StudentId && e.CourseId == courseId);

                if (!alreadyEnrolled)
                {
                    var enrollment = new Enrollment
                    {
                        StudentId = registration.StudentId,
                        CourseId = courseId,
                        SemesterId = latestSemester.Id
                    };

                    _context.Enrollments.Add(enrollment);
                }
            }

            await _context.SaveChangesAsync();
        }
        
    }
}
