using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace IniFileTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IniFile iniFile = new IniFile("TestIniFile.ini");

            //Write a int32 value
            iniFile.WriteValue("section1", "key1", 42);

            //Write a string value
            iniFile.WriteValue("section1", "key2", "This is a test");

            //Write a double value
            iniFile.WriteValue("section2", "key3", 16.84);

			//Write a boolean value
			iniFile.WriteValue("section2", "key4", true);

            //Read section/key names
            Console.WriteLine("File sections/keys");
            Console.WriteLine("-------------------");

            string[] sections = iniFile.GetSectionNames();
            foreach (string section in sections)
            {
                Console.WriteLine("[" + section + "]");

                string[] keys = iniFile.GetKeyNames(section);

                foreach (string key in keys)
                {
                    Console.WriteLine(key);
                }
            }

            Console.WriteLine("\n-------------------\n");

            //Read int32 value.
            int value1 = iniFile.GetInt32("section1", "key1", 0);
            Console.WriteLine("key1 = " + value1);

            //Read string value.
            string value2 = iniFile.GetString("section1", "key2", "test");
            Console.WriteLine("key2 = " + value2);

            //Read double value.
            double value3 = iniFile.GetDouble("section2", "key3", 0.0);
            Console.WriteLine("key3 = " + value3);

			//Read double value.
			bool value4 = iniFile.GetBoolean("section2", "key4", false);
			Console.WriteLine("key4 = " + value4);

            Console.WriteLine("\n-------------------\n");

            //Delete value key2
            Console.WriteLine("Deleting section1/key2");
            iniFile.DeleteKey("section1", "key2");

            value2 = iniFile.GetString("section1", "key2", "");
            Console.WriteLine("key2=" + value2);

            //Delete section2
            Console.WriteLine("Deleting section2");
            iniFile.DeleteSection("section2");

            value3 = iniFile.GetDouble("section2", "key3", 0.0);
            Console.WriteLine("key3=" + value3);

            Console.WriteLine("\n-------------------\n");

            //Test that calling WriteValue with null section, keyName, or 
            //value causes a exception
            try
            {
                iniFile.WriteValue(null, "key1", "test");
                Console.WriteLine("*** No exception on invalid section name ***");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("WriteValue correctly threw an exception on null sectionName.");    
            }

            try
            {
                iniFile.WriteValue("section1", null, "test");
                Console.WriteLine("*** No exception on invalid key name ***");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("WriteValue correctly threw an exception on null keyName.");
            }

            try
            {
                iniFile.WriteValue("section1", "key2", null);
                Console.WriteLine("*** No exception on invalid value ***");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("WriteValue correctly threw an exception on null value.");
            }      
        }
    }
}
