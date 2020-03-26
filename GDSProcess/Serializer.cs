﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GDSProcess
{
    public class Serializer
    {
        public T Deserialize<T>(string input) where T : class
        {
            try
            {
                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

                using (StringReader sr = new StringReader(input))
                {
                    return (T)ser.Deserialize(sr);
                }
            }
            catch (Exception ex)

            {
                throw ex;
                return null;
            }

        }

        public string Serialize<T>(T ObjectToSerialize)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                    return textWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
    }
}
