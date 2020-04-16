using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileIdCreator {
    class Guard {
        public static void throwIfNull(object argumentValue,string argumentName) {
            if (argumentValue == null) {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void throwIfStringNullOrEmpty(string argumentValue, string argumentName) {
            if (argumentValue == null || string.IsNullOrEmpty(argumentValue) || string.IsNullOrWhiteSpace(argumentValue)) {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
