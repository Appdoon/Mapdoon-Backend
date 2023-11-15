﻿using Mapdoon.Application.Services.GradeHomeworks.Command.SubmitScoreService;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Users;
using FluentAssertions;

namespace Mapdoon.Application.Tests.GradeHomeworks.Command
{

    using static Testing;
    public class SubmitScoreTests : TestBase
    {
        [Test]
        public void ShouldSubmitScore()
        {
            var userId1 = AddEntity(new User
            {
                Email = "aysa@gmail.com",
                Password = "password",
            });
            var userId2 = AddEntity(new User
            {
                Email = "golsa@gmail.com",
                Password = "password",
            });
            var homeworkId = AddEntity(new Homework
            {
                Title = "Title",
                CreatorId = userId1,
                MinScore = 100,
            });
            var submission = new HomeworkProgressSubmissionDto
            {
                HomeworkId = homeworkId,
                UserId = userId1,
                Score = 50,
                MinScore = 100,
            };

            var result = new SubmitScoreService(GetDatabaseContext()).Execute(submission);
            result.IsSuccess.Should().Be(true);

        }
    }
}