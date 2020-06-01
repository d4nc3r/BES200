using LibraryApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApiIntegrationTests.Fakes
{
    public class FakeSystemTime : ISystemTime
    {
        public DateTime GetCurrent()
        {
            return new DateTime(1979, 01, 22, 16, 20, 00);
        }
    }
}
