using E2E.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2E.TestData
{
    public static class ClientsData
    {
        public static readonly Client JohnTester = new Client(
            "John",
            "Tester",
            "12345"
        );
    }
}
