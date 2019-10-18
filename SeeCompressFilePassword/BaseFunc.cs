using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Models;
using RestSharp;
using System.Net;



using System.ComponentModel;
using System.Security.Cryptography;
using System.Threading;
using System.Collections;

using System.IO;






using SCFP_Compress.Reader.Rar;
using SCFP_Compress.Common;
using SCFP_Compress.Common.Rar.Headers;
using SCFP_Compress.IO;

using SCFP_Compress.Reader;

using SCFP_Compress.Archive;
using SCFP_Compress.Archive.Rar;

using SCFP_Compress.Reader.Zip;
using SCFP_Compress.Writer;
using libcomm;


namespace SCFP_Compress.SeeCompressFilePassword
{
    public static class BaseFunc
    {

        ///// <summary>
        ///// RESTSCFP_Global_userMinScoreURL
        ///// </summary>

        //public const string RESTSCFP_Global_userMinScoreURL = RESTBaseURL + "globalOption/userMinScore";
        //public const string RESTSCFP_Global_userMinScoreURL_Short = "globalOption/userMinScore";



        ///// <summary>
        ///// RESTSCFP_Global_userDefaultTaskScoreURL
        ///// </summary>

        //public const string RESTSCFP_Global_userDefaultTaskScoreURL = RESTBaseURL + "globalOption/userDefaultTaskScore";
        //public const string RESTSCFP_Global_userDefaultTaskScoreURL_Short = "globalOption/userDefaultTaskScore";




        ///// <summary>
        ///// RESTSCFP_Global_userDefaultTaskScoreCostURL
        ///// </summary>

        //public const string RESTSCFP_Global_userDefaultTaskScoreCostURL = RESTBaseURL + "globalOption/userDefaultTaskScoreCost";
        //public const string RESTSCFP_Global_userDefaultTaskScoreCostURL_Short = "globalOption/userDefaultTaskScoreCost";


        ///// <summary>
        ///// RESTSCFP_Global_AppVersionURL
        ///// </summary>

        //public const string RESTSCFP_Global_AppVersionURL = RESTBaseURL + "globalOption/appVersion";
        //public const string RESTSCFP_Global_AppVersionURL_Short = "globalOption/appVersion";

        /// <summary>
        /// Get_Global_userMinScore
        /// </summary>
        /// 

        public static bool IS_Vaild_Email(string Email)
        {

            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            if (!r.IsMatch(Email))
            {
                return false;
            }
            return true;
       }

        public static bool IS_Vaild_WebURL(string WebURL)
        {

            Regex r = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            if (!r.IsMatch(WebURL))
            {
                return false;
            }
            return true;
        }

        public static int Get_Global_userMinScore()
        {
            return GetIntValueFromURL(BaseConstants.RESTBaseURL, BaseConstants.RESTSCFP_Global_userMinScoreURL_Short);

        }

        public static int Get_Global_userDefaultTaskScoreCost()
        {
            return GetIntValueFromURL(BaseConstants.RESTBaseURL, BaseConstants.RESTSCFP_Global_userDefaultTaskScoreCostURL_Short);

        }

        public static int Get_Global_userDefaultTaskScore()
        {
            return GetIntValueFromURL(BaseConstants.RESTBaseURL, BaseConstants.RESTSCFP_Global_userDefaultTaskScoreURL_Short);

        }


        public static int Get_Global_AppVersion()
        {
            return GetIntValueFromURL(BaseConstants.RESTBaseURL, BaseConstants.RESTSCFP_Global_AppVersionURL_Short);

        }


        public static string Get_Global_AppADURL()
        {
            return GetStringValueFromURL(BaseConstants.RESTBaseURL, BaseConstants.RESTSCFP_Global_AppADURL_Short);

        }


        public static string Get_Global_AppVIPTopListURL_Short()
        {
            return GetStringValueFromURL(BaseConstants.RESTBaseURL, BaseConstants.RESTSCFP_Global_AppVIPTopListURL_Short);

        }



        


            private static int GetIntValueFromURL(string BaseURL,string ReqURL)
            {

                try
                {


                    var client = new RestClient(BaseURL);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);

                    var request = new RestRequest(ReqURL, Method.GET);



                    IRestResponse response = client.Execute(request);


                    if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
              (response.ResponseStatus == ResponseStatus.Completed))) // It's probably not necessary to test both
                    {


                        //var returnuser = new SCFP_user_send();
                        var content = response.Content; // raw content as string
                        var returnuser = SimpleJson.DeserializeObject<SCFP_global>(content);

                        //SimpleJson.DeserializeObject(content, returnuser);
                        if (returnuser.returnCode == 200)
                        {
                            int ivalue = int.Parse(returnuser.Global_Value);
                            return ivalue;
                        }
                        else
                        {
                            return 0;
                            //return defualt value
                        }

                    }
                    else if (response != null)
                    {
                        return 0;
                        //return defualt value
                        //MessageBox.Show(string.Format
                        //("Status code is {0} ({1}); response status is {2}",
                        //       response.StatusCode, response.StatusDescription, response.ResponseStatus));
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                    return 0;
                    //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show(ex.Message);
                }


            }


            private static string GetStringValueFromURL(string BaseURL, string ReqURL)
            {

                try
                {


                    var client = new RestClient(BaseURL);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);

                    var request = new RestRequest(ReqURL, Method.GET);



                    IRestResponse response = client.Execute(request);


                    if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
              (response.ResponseStatus == ResponseStatus.Completed))) // It's probably not necessary to test both
                    {


                        //var returnuser = new SCFP_user_send();
                        var content = response.Content; // raw content as string
                        var returnuser = SimpleJson.DeserializeObject<SCFP_global>(content);

                        //SimpleJson.DeserializeObject(content, returnuser);
                        if (returnuser.returnCode == 200)
                        {
                            
                            return returnuser.Global_Value;
                        }
                        else
                        {
                            return string.Empty;
                            //return defualt value
                        }

                    }
                    else if (response != null)
                    {
                        return string.Empty;
                        //return defualt value
                        //MessageBox.Show(string.Format
                        //("Status code is {0} ({1}); response status is {2}",
                        //       response.StatusCode, response.StatusDescription, response.ResponseStatus));
                    }
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    throw ex;
                    return string.Empty;
                    //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show(ex.Message);
                }


            }

        public static SCFP_user_recv GetCurrentInfo(string UserName,string Password)
        {

            try
            {
                var user = new SCFP_user_send();



                user.id = -1;
                user.username = UserName;
                user.email = "";
                user.password = Password;


                var client = new RestClient(BaseConstants.RESTBaseURL);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest(BaseConstants.RESTSCFP_userURL_Short, Method.POST);

                request.AddParameter("username", user.username); // adds to POST or URL querystring based on Method
                request.AddParameter("email", user.email); // adds to POST or URL querystring based on Method
                request.AddParameter("password", user.password); // adds to POST or URL querystring based on Method

                IRestResponse response = client.Execute(request);


                if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
          (response.ResponseStatus == ResponseStatus.Completed))) // It's probably not necessary to test both
                {


                    //var returnuser = new SCFP_user_send();
                    var content = response.Content; // raw content as string
                    var returnuser = SimpleJson.DeserializeObject<SCFP_user_recv>(content);


                    return returnuser;
                    
            

                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
                //MessageBox.Show(ex.Message);
            }
            
        }



        public static string GetExtFileNameByString(string sFileName)
        {
            return sFileName.Substring(sFileName.LastIndexOf("."), sFileName.Length - sFileName.LastIndexOf(".")).ToLower();
        }

        public static bool CheckZipPasswordVaild(string FilePath,string Password)
        {
            //ResetScratch();
            bool isEncrypt = false;
            try
            {
                using (Stream stream = File.OpenRead(FilePath))
                using (var reader = ZipReader.Open(stream,Password))
                {
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            //Assert.AreEqual(reader.Entry.CompressionType, CompressionType.Unknown);
                            //reader.WriteEntryToDirectory(SCRATCH_FILES_PATH,
                            //                             ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                isEncrypt = true;
                //此处对未知文件头进行处理
                if (ex.Message.ToString().Contains("Unknown header"))
                {
                    isEncrypt = false;
                }

            }
            return isEncrypt;

        }
        public static bool CheckZipEncrypted(string FilePath)
        {
            //ResetScratch();
            bool isEncrypt = false;
            try
            {
                using (Stream stream = File.OpenRead(FilePath))
                using (var reader = ZipReader.Open(stream))
                {
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            //Assert.AreEqual(reader.Entry.CompressionType, CompressionType.Unknown);
                            //reader.WriteEntryToDirectory(SCRATCH_FILES_PATH,
                            //                             ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                isEncrypt = true;
                //此处对未知文件头进行处理
                if (ex.Message.ToString().Contains("Unknown header"))
                {
                    isEncrypt = false;
                }
                
            }
            return isEncrypt;

        }
        //public void VerifyFiles()
        //{
        //    if (UseExtensionInsteadOfNameToVerify)
        //    {
        //        VerifyFilesByExtension();
        //    }
        //    else
        //    {
        //        VerifyFilesByName();
        //    }
        //}

        //protected void VerifyFilesByName()
        //{
        //    var extracted =
        //        Directory.EnumerateFiles(SCRATCH_FILES_PATH, "*.*", SearchOption.AllDirectories)
        //        .ToLookup(path => path.Substring(SCRATCH_FILES_PATH.Length));
        //    var original =
        //        Directory.EnumerateFiles(ORIGINAL_FILES_PATH, "*.*", SearchOption.AllDirectories)
        //        .ToLookup(path => path.Substring(ORIGINAL_FILES_PATH.Length));

        //    Assert.AreEqual(extracted.Count, original.Count);

        //    foreach (var orig in original)
        //    {
        //        Assert.IsTrue(extracted.Contains(orig.Key));

        //        CompareFilesByPath(orig.Single(), extracted[orig.Key].Single());
        //    }
        //}

        //protected bool UseExtensionInsteadOfNameToVerify { get; set; }

        //protected void VerifyFilesByExtension()
        //{
        //    var extracted =
        //        Directory.EnumerateFiles(SCRATCH_FILES_PATH, "*.*", SearchOption.AllDirectories)
        //        .ToLookup(path => Path.GetExtension(path));
        //    var original =
        //        Directory.EnumerateFiles(ORIGINAL_FILES_PATH, "*.*", SearchOption.AllDirectories)
        //        .ToLookup(path => Path.GetExtension(path));

        //    Assert.AreEqual(extracted.Count, original.Count);

        //    foreach (var orig in original)
        //    {
        //        Assert.IsTrue(extracted.Contains(orig.Key));

        //        CompareFilesByPath(orig.Single(), extracted[orig.Key].Single());
        //    }
        //}


        public static void VerifyFiles()
        {
            if (UseExtensionInsteadOfNameToVerify)
            {
                VerifyFilesByExtension();
            }
            else
            {
                VerifyFilesByName();
            }
        }

        public static void VerifyFilesByName()
        {
            var extracted =
                Directory.EnumerateFiles("SCRATCH_FILES_PATH", "*.*", SearchOption.AllDirectories)
                .ToLookup(path => path.Substring("SCRATCH_FILES_PATH".Length));
            var original =
                Directory.EnumerateFiles("ORIGINAL_FILES_PATH", "*.*", SearchOption.AllDirectories)
                .ToLookup(path => path.Substring("ORIGINAL_FILES_PATH".Length));

           // Assert.AreEqual(extracted.Count, original.Count);

            foreach (var orig in original)
            {
                //Assert.IsTrue(extracted.Contains(orig.Key));

                CompareFilesByPath(orig.Single(), extracted[orig.Key].Single());
            }
        }

        public static bool UseExtensionInsteadOfNameToVerify { get; set; }

        public static void VerifyFilesByExtension()
        {
            var extracted =
                Directory.EnumerateFiles("SCRATCH_FILES_PATH", "*.*", SearchOption.AllDirectories)
                .ToLookup(path => Path.GetExtension(path));
            var original =
                Directory.EnumerateFiles("ORIGINAL_FILES_PATH", "*.*", SearchOption.AllDirectories)
                .ToLookup(path => Path.GetExtension(path));

            //Assert.AreEqual(extracted.Count, original.Count);

            foreach (var orig in original)
            {
                //Assert.IsTrue(extracted.Contains(orig.Key));

                CompareFilesByPath(orig.Single(), extracted[orig.Key].Single());
            }
        }

        public static void CompareFilesByPath(string file1, string file2)
        {
            using (var file1Stream = File.OpenRead(file1))
            using (var file2Stream = File.OpenRead(file2))
            {
               // Assert.AreEqual(file1Stream.Length, file2Stream.Length);
                int byte1 = 0;
                int byte2 = 0;
                for (int counter = 0; byte1 != -1; counter++)
                {
                    byte1 = file1Stream.ReadByte();
                    byte2 = file2Stream.ReadByte();
                    if (byte1 != byte2)
                    { 
                       // Assert.AreEqual(byte1, byte2, string.Format("Byte {0} differ between {1} and {2}",
                       //     counter, file1, file2));
                    }
                }
            }
        }

        public static void CompareArchivesByPath(string file1, string file2)
        {
            using (var archive1 = ReaderFactory.Open(File.OpenRead(file1), Options.None))
            using (var archive2 = ReaderFactory.Open(File.OpenRead(file2), Options.None))
            {
                while (archive1.MoveToNextEntry())
                {
                    //Assert.IsTrue(archive2.MoveToNextEntry());
                    // Assert.AreEqual(archive1.Entry.Key, archive2.Entry.Key);
                }
               // Assert.IsFalse(archive2.MoveToNextEntry());
            }
        }


        public static bool CheckRARPasswordVaild(string FilePath,string Password)
        {
            //SOLUTION_BASE_PATH = Path.GetDirectoryName(Path.GetDirectoryName(ctx.TestDir));
            //TEST_ARCHIVES_PATH = Path.Combine(SOLUTION_BASE_PATH, "TestArchives", "Archives");
            //ORIGINAL_FILES_PATH = Path.Combine(SOLUTION_BASE_PATH, "TestArchives", "Original");
            //MISC_TEST_FILES_PATH = Path.Combine(SOLUTION_BASE_PATH, "TestArchives", "MiscTest");
            //SCRATCH_FILES_PATH = Path.Combine(SOLUTION_BASE_PATH, "TestArchives", "Scratch");
            //SCRATCH2_FILES_PATH = Path.Combine(SOLUTION_BASE_PATH, "TestArchives", "Scratch2");

            bool isEncrypt = false;
            try
            {
                using (Stream stream = File.OpenRead(FilePath))
                using (var archive = RarArchive.Open(stream,Options.None,Password))
                {
                    string sPath = System.IO.Path.GetTempPath() + System.Guid.NewGuid().ToString();
                    string sArcFile = string.Empty;
                    System.IO.Directory.CreateDirectory(sPath);
                    long archfilesize = 0;
                    long destfilesize = 0;
                    long archfileCRC32 = 0;
                    long destfileCRC32 = 0;

                    string archfileCRC32_str = string.Empty;
                    string destfileCRC32_str = string.Empty;
                    //archive.WriteToDirectory(sPath);

                    foreach (var entry in archive.Entries)
                    {
                        
                    //    Stream entrywriteto = null ;
                    //    long FileCRC = entry.Crc;
                    //    System.IO.Directory.CreateDirectory(sPath);
                    //    //entry.WriteToFile(, ExtractOptions.Overwrite);
                    //    //archive.WriteToDirectory(, ExtractOptions.Overwrite);
                        if(!entry.IsDirectory)
                        { //随便找一个文件解压，然后比较文件大小，不一样代表密码错误

                            sArcFile = Path.Combine(sPath, entry.FileHeader.FileName.ToString());
                            archfileCRC32 = entry.Crc;
                            archfileCRC32_str = entry.Crc.ToString("x8");
                            archfilesize = entry.Size;
                            entry.WriteToFile(sArcFile, ExtractOptions.Overwrite);

                            break;
                        }
                    }

                    FileInfo fileInfo = new FileInfo(sArcFile);
                    destfilesize = fileInfo.Length;
                    //destfileCRC32_str = GetFileCRC32(sArcFile);

                    byte[] rb = File.ReadAllBytes(sArcFile);

                   destfileCRC32 =(long) (CRC32.ComputeChecksum(rb));

                   destfileCRC32_str = destfileCRC32.ToString("X");
                    //destfileCRC32_str = MyHash(Encoding.UTF8.GetString(rb), "CRC32");



                    if(archfilesize != destfilesize)
                    {
                        isEncrypt = true;
                    }

                }
            }
            catch (Exception ex)
            {
                isEncrypt = true;
            }
            return isEncrypt;

        }

      public  static string MyHash (string input, string type)
		{
			byte[] bytes = Encoding.UTF8.GetBytes (input);
			string output = null;
			
			switch (type) {
			case "MD5":
				byte[] a0 = MD5.Create ().ComputeHash (bytes);
				StringBuilder sb = new StringBuilder ();
				
				for (int i = 0; i < a0.Length; i++) {
					sb.Append (a0 [i].ToString ("X2").ToLower ());
				}
				output = sb.ToString ();
				break;
				
			case "SHA128":
				byte[] a1 = SHA1.Create ().ComputeHash (bytes);
				
				for (int j = 0; j < a1.Length; j++) {
					byte b0 = a1 [j];
					output = output + ((b0.ToString ("X").ToLower ().Length != 1) ? "" : "0") + b0.ToString ("X").ToLower ();
				}
				break;
				
			case "SHA256":
				byte[] a2 = SHA256.Create ().ComputeHash (bytes);
				
				for (int k = 0; k < a2.Length; k++) {
					byte b1 = a2 [k];
					output += string.Format ("{0:x2}", b1);
				}
				break;
				
			case "SHA512":
				byte[] a3 = SHA512.Create ().ComputeHash (bytes);
				
				for (int l = 0; l < a3.Length; l++) {
					byte b2 = a3 [l];
					output += string.Format ("{0:x2}", b2);
				}
				break;
				
			case "CRC32":
				char[] a4 = input.ToCharArray ();
				for (int m = 0; m < a4.Length; m++) {
					if (a4 [m] <= '\u007f') {
						a4 [m] = char.ToLowerInvariant (a4 [m]);
					}
				}
				
				string s = new string (a4);
				uint n = 4294967295u;
				byte[] a5 = Encoding.UTF8.GetBytes (s);
				for(int y = 0; y < a5.Length; y++) {
					byte b3 = a5[y];
					n ^= (uint)((uint)b3 << 24);
					for(int y0 = 0; y0 < 8; y0++) {
						if((Convert.ToUInt32 (n) & 2147483648u) == 2147483648u)	{
							n = ( n << 1 ^ 79764919u);
						} 
						else {
							n <<= 1;
						}
					}
				}
				output = string.Format ("{0:x8}", n);
				break;					
				
			default:
				break;
			}
			
			return output;
		}


        private static UInt32[] Crc32Table = {   
                                  0x00000000,0x77073096,0xEE0E612C,0x990951BA,  
                                  0x076DC419,0x706AF48F,0xE963A535,0x9E6495A3,  
                                  0x0EDB8832,0x79DCB8A4,0xE0D5E91E,0x97D2D988,  
                                  0x09B64C2B,0x7EB17CBD,0xE7B82D07,0x90BF1D91,  
                                  0x1DB71064,0x6AB020F2,0xF3B97148,0x84BE41DE,  
                                  0x1ADAD47D,0x6DDDE4EB,0xF4D4B551,0x83D385C7,  
                                  0x136C9856,0x646BA8C0,0xFD62F97A,0x8A65C9EC,  
                                  0x14015C4F,0x63066CD9,0xFA0F3D63,0x8D080DF5,  
                                  0x3B6E20C8,0x4C69105E,0xD56041E4,0xA2677172,  
                                  0x3C03E4D1,0x4B04D447,0xD20D85FD,0xA50AB56B,  
                                  0x35B5A8FA,0x42B2986C,0xDBBBC9D6,0xACBCF940,  
                                  0x32D86CE3,0x45DF5C75,0xDCD60DCF,0xABD13D59,  
                                  0x26D930AC,0x51DE003A,0xC8D75180,0xBFD06116,  
                                  0x21B4F4B5,0x56B3C423,0xCFBA9599,0xB8BDA50F,  
                                  0x2802B89E,0x5F058808,0xC60CD9B2,0xB10BE924,  
                                  0x2F6F7C87,0x58684C11,0xC1611DAB,0xB6662D3D,  
                                  0x76DC4190,0x01DB7106,0x98D220BC,0xEFD5102A,  
                                  0x71B18589,0x06B6B51F,0x9FBFE4A5,0xE8B8D433,  
                                  0x7807C9A2,0x0F00F934,0x9609A88E,0xE10E9818,  
                                  0x7F6A0DBB,0x086D3D2D,0x91646C97,0xE6635C01,  
                                  0x6B6B51F4,0x1C6C6162,0x856530D8,0xF262004E,  
                                  0x6C0695ED,0x1B01A57B,0x8208F4C1,0xF50FC457,  
                                  0x65B0D9C6,0x12B7E950,0x8BBEB8EA,0xFCB9887C,  
                                  0x62DD1DDF,0x15DA2D49,0x8CD37CF3,0xFBD44C65,  
                                  0x4DB26158,0x3AB551CE,0xA3BC0074,0xD4BB30E2,  
                                  0x4ADFA541,0x3DD895D7,0xA4D1C46D,0xD3D6F4FB,  
                                  0x4369E96A,0x346ED9FC,0xAD678846,0xDA60B8D0,  
                                  0x44042D73,0x33031DE5,0xAA0A4C5F,0xDD0D7CC9,  
                                  0x5005713C,0x270241AA,0xBE0B1010,0xC90C2086,  
                                  0x5768B525,0x206F85B3,0xB966D409,0xCE61E49F,  
                                  0x5EDEF90E,0x29D9C998,0xB0D09822,0xC7D7A8B4,  
                                  0x59B33D17,0x2EB40D81,0xB7BD5C3B,0xC0BA6CAD,  
                                  0xEDB88320,0x9ABFB3B6,0x03B6E20C,0x74B1D29A,  
                                  0xEAD54739,0x9DD277AF,0x04DB2615,0x73DC1683,  
                                  0xE3630B12,0x94643B84,0x0D6D6A3E,0x7A6A5AA8,  
                                  0xE40ECF0B,0x9309FF9D,0x0A00AE27,0x7D079EB1,  
                                  0xF00F9344,0x8708A3D2,0x1E01F268,0x6906C2FE,  
                                  0xF762575D,0x806567CB,0x196C3671,0x6E6B06E7,  
                                  0xFED41B76,0x89D32BE0,0x10DA7A5A,0x67DD4ACC,  
                                  0xF9B9DF6F,0x8EBEEFF9,0x17B7BE43,0x60B08ED5,  
                                  0xD6D6A3E8,0xA1D1937E,0x38D8C2C4,0x4FDFF252,  
                                  0xD1BB67F1,0xA6BC5767,0x3FB506DD,0x48B2364B,  
                                  0xD80D2BDA,0xAF0A1B4C,0x36034AF6,0x41047A60,  
                                  0xDF60EFC3,0xA867DF55,0x316E8EEF,0x4669BE79,  
                                  0xCB61B38C,0xBC66831A,0x256FD2A0,0x5268E236,  
                                  0xCC0C7795,0xBB0B4703,0x220216B9,0x5505262F,  
                                  0xC5BA3BBE,0xB2BD0B28,0x2BB45A92,0x5CB36A04,  
                                  0xC2D7FFA7,0xB5D0CF31,0x2CD99E8B,0x5BDEAE1D,  
                                  0x9B64C2B0,0xEC63F226,0x756AA39C,0x026D930A,  
                                  0x9C0906A9,0xEB0E363F,0x72076785,0x05005713,  
                                  0x95BF4A82,0xE2B87A14,0x7BB12BAE,0x0CB61B38,  
                                  0x92D28E9B,0xE5D5BE0D,0x7CDCEFB7,0x0BDBDF21,  
                                  0x86D3D2D4,0xF1D4E242,0x68DDB3F8,0x1FDA836E,  
                                  0x81BE16CD,0xF6B9265B,0x6FB077E1,0x18B74777,  
                                  0x88085AE6,0xFF0F6A70,0x66063BCA,0x11010B5C,  
                                  0x8F659EFF,0xF862AE69,0x616BFFD3,0x166CCF45,  
                                  0xA00AE278,0xD70DD2EE,0x4E048354,0x3903B3C2,  
                                  0xA7672661,0xD06016F7,0x4969474D,0x3E6E77DB,  
                                  0xAED16A4A,0xD9D65ADC,0x40DF0B66,0x37D83BF0,  
                                  0xA9BCAE53,0xDEBB9EC5,0x47B2CF7F,0x30B5FFE9,  
                                  0xBDBDF21C,0xCABAC28A,0x53B39330,0x24B4A3A6,  
                                  0xBAD03605,0xCDD70693,0x54DE5729,0x23D967BF,  
                                  0xB3667A2E,0xC4614AB8,0x5D681B02,0x2A6F2B94,  
                                  0xB40BBE37,0xC30C8EA1,0x5A05DF1B,0x2D02EF8D};

        /// <summary>  
        /// 获取文件的CRC32标识  
        /// </summary>  
        /// <param name="filePath"></param>  
        /// <returns></returns>  
        public static String GetFileCRC32(String filePath)
        {
            const string FOO = "-";
            if (string.IsNullOrEmpty(filePath))
            {
                return FOO;
            }
            if (!File.Exists(filePath))
            {
                return FOO;
            }

            // 最大50M  
            const int MAX_SIZE = 50 * 1024 * 1024;
            var f = new FileInfo(filePath);
            if (f.Length >= MAX_SIZE)
            {
                return FOO;
            }

            UInt32 crc = 0xFFFFFFFF;
            var bin = File.ReadAllBytes(filePath);
            foreach (byte b in bin)
            {
                crc = ((crc >> 8) & 0x00FFFFFF) ^ Crc32Table[(crc ^ b) & 0xFF];
            }
            crc = crc ^ 0xFFFFFFFF;
            //return crc.ToString("X");
            return crc.ToString("x8");
        }  

        public static bool CheckRAREncrypted(string FilePath)
        {
            bool isEncrypt = false;
            try
            {
                using (Stream stream = File.OpenRead(FilePath))
                using (var archive = RarArchive.Open(stream))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (entry.IsEncrypted)
                        {
                            isEncrypt = true;
                        }
                        else
                        {
                            isEncrypt = false;
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                isEncrypt = true;
            }
            return isEncrypt;

        }

        private static FileStream GetReaderStream(string FilePath)
        {
            return new FileStream(FilePath,
                                  FileMode.Open);
        }

        public static bool CheckEncryptedFlag(string FilePath)
        {


            bool isEncrypt = false;

            try
            {
                RarHeaderFactory rarHeaderFactory;
                rarHeaderFactory = new RarHeaderFactory(StreamingMode.Seekable, Options.KeepStreamsOpen);
                using (var stream = GetReaderStream(FilePath))
                    foreach (var header in rarHeaderFactory.ReadHeaders(stream))
                    {
                        if (header.HeaderType == HeaderType.ArchiveHeader)
                        {
                            if (rarHeaderFactory.IsEncrypted)
                            {
                                isEncrypt = true;
                            }
                            else
                            {
                                isEncrypt = false;
                            }
                            //Assert.AreEqual(isEncrypted, rarHeaderFactory.IsEncrypted);
                            break;
                        }
                    }

            }
            catch (Exception ex)
            {
                isEncrypt = true;
            }


            return isEncrypt;

        }



        #region 获取由SHA1加密的字符串
        public static string EncryptToSHA1(string str)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] str1 = Encoding.UTF8.GetBytes(str);
            byte[] str2 = sha1.ComputeHash(str1);
            sha1.Clear();
            (sha1 as IDisposable).Dispose();
            return Convert.ToBase64String(str2);
        }
        #endregion
        #region 获取由MD5加密的字符串
        public static string EncryptToMD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] str1 = Encoding.UTF8.GetBytes(str);
            byte[] str2 = md5.ComputeHash(str1, 0, str1.Length);
            md5.Clear();
            (md5 as IDisposable).Dispose();
            return Convert.ToBase64String(str2);
        }
        #endregion

    }

    
}
