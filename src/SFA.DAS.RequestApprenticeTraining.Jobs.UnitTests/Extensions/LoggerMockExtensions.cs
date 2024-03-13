﻿using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.UnitTests.Extensions
{
    public static class LoggerMockExtensions
    {
        public static void VerifyLogging<T>(this Mock<ILogger<T>> loggerMock, LogLevel logLevel, Times times)
        {
            loggerMock.Verify(l => l.Log(logLevel,
               It.IsAny<EventId>(),
               It.Is<object>(o => o != null),
               It.IsAny<Exception>(),
               (Func<object, Exception, string>)It.IsAny<object>()),
               times);
        }
    }
}