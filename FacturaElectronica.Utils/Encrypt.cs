using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Security.Cryptography;

namespace FacturaElectronica.Utils
{
	/// <summary>
	/// Summary description for Encrypt.
	/// </summary>
	public class Encrypt
	{
		public Encrypt()
		{
		}

		public static string GetData(string strFilePath)
		{
			UTF8Encoding textConverter   = new UTF8Encoding();
			RijndaelManaged myRijndael   = new RijndaelManaged();
			ICryptoTransform decryptor	 = null;
			FileStream fsDecrypt		 = null;
			CryptoStream csDecrypt		 = null;
			StreamReader objStreamReader = null;
			string strResult			 = string.Empty;

			byte[] Key = {0x45, 0x22, 0x72, 0x08, 0x13, 0x95, 0x32, 0x14, 0x19, 0x68, 0x43, 0x20, 0x18, 0x12, 0x11, 0x96};
			byte[] IV = {0x18, 0x73, 0x24, 0x18, 0x33, 0x92, 0x20, 0x17, 0x12, 0x99, 0x18, 0x14, 0x13, 0x62, 0x16, 0x17};

			try
			{
				//Get a decryptor that uses the same key and IV as the encryptor.
				decryptor = myRijndael.CreateDecryptor(Key, IV);

				//Now decrypt the previously encrypted message using the decryptor
				//obtained in the above step.
				fsDecrypt = new FileStream(strFilePath,System.IO.FileMode.Open);
				csDecrypt = new CryptoStream(fsDecrypt, decryptor, CryptoStreamMode.Read);

				//Read the data out of the crypto stream.
				objStreamReader =  new StreamReader(csDecrypt);	
				
				strResult = objStreamReader.ReadToEnd();		
				
			}
			catch(Exception ex)
			{		
				throw (ex);
			}
			finally
			{
				textConverter = null;
				if(myRijndael!=null)myRijndael.Clear();
				myRijndael = null;
				if(decryptor!=null)	decryptor.Dispose();
				decryptor = null;
				if(fsDecrypt!=null)fsDecrypt.Close();
				fsDecrypt = null;
				if(csDecrypt!=null)csDecrypt.Close();
				if(objStreamReader!=null)objStreamReader.Close();

				Key = null;
				IV = null;
			} 

			return  strResult;
		}

		public static void SaveData(string strXmlData, string strFilePath)
		{
			UTF8Encoding textConverter	= new UTF8Encoding();
			RijndaelManaged myRijndael	= new RijndaelManaged();
			byte[] toEncrypt;
			byte[] Key = {0x45, 0x22, 0x72, 0x08, 0x13, 0x95, 0x32, 0x14, 0x19, 0x68, 0x43, 0x20, 0x18, 0x12, 0x11, 0x96};
			byte[] IV = {0x18, 0x73, 0x24, 0x18, 0x33, 0x92, 0x20, 0x17, 0x12, 0x99, 0x18, 0x14, 0x13, 0x62, 0x16, 0x17};

			XmlDocument xmlData			= new XmlDocument();

			try
			{
				xmlData.LoadXml( strXmlData );

				//Get an encryptor.
				ICryptoTransform encryptor = myRijndael.CreateEncryptor(Key, IV);
            
				//Delete file if exists
				FileInfo fi = new FileInfo(strFilePath);
				if(fi.Exists == true)
				{
					fi.Delete();
				}

				//Encrypt the data.
				FileStream fsEncrypt = new System.IO.FileStream(strFilePath, System.IO.FileMode.CreateNew);
				CryptoStream csEncrypt = new CryptoStream(fsEncrypt, encryptor, CryptoStreamMode.Write);

				//Convert the data to a byte array.
				toEncrypt = textConverter.GetBytes(xmlData.InnerXml);

				//Write all data to the crypto stream and flush it.
				csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
				csEncrypt.FlushFinalBlock();

				fsEncrypt.Flush();
				fsEncrypt.Close();
			}
			catch(Exception ex)
			{		
				throw (ex);
			}
			finally
			{				
				textConverter	= null;
				Key				= null;
				IV				= null;
			} 
		}

		public static string EncryptPassword(string strDataToEncrypt,string strXmlPublicKeyInfo)
		{
			//define data objects			
			CspParameters CSPParam;
			RSACryptoServiceProvider RSA;
			UnicodeEncoding ByteConverter;
			string strEncryptedData;
			
			try
			{
				strXmlPublicKeyInfo = "<RSAKeyValue><Modulus>xBD7dzFwBQlbYnivkNWP058VPrYouYvVpT/PhSjZzYwPXjV3G/G8zSJ9W07z5U/9ltlpmoD8E8oTftdwBLzcliQLoN9wEYHyEFK83vS2GFs4nNbYFsXmIEQv9nCi7pWdmqShLVcZyIMRoRmx5xvxYXbzZfuMsBJp+24ddIOSkQE=</Modulus><Exponent>AQAB</Exponent><P>4FGK7v1qDDoGFE6A+zAhkhu78qJoQJbjuHosLHpWQOsklrkDS/53oAeymI7U5TpSIW5m2kn/Njnjd0UDWJaI8Q==</P><Q>38H0L2pGOUoOMcZQj7gN3ipufs3yz+qhxtO616UgcJRzwVX30f/4jHn0AN/Qsd0s/JfA47AU/+/KBwFv+vMJEQ==</Q><DP>nn6/LfH8VjtBRGPbmp/9eGEEQYIjK0/yAszAHYUrIJ+EsMJ04+Me3wHRXR2+q2Cz209IeQBDxSrN393xaUyj4Q==</DP><DQ>bu5uTA+JDx/614x1JjtejLVGoJEj/xZY7ATOTdjss0O1+7TOLwuTQrm3UbOkeHmpsvTdSURjtNtBsqVwyd4xsQ==</DQ><InverseQ>LjYNBt78I0qw6PWWq6qPGorAnCLNYpmkMqniSMkR7kAB2xW7hK/D3Em/qPpY3VmttJCCWuqQ14Q4JgKWdkhPGg==</InverseQ><D>DfNDYSS0WrdiuCvsGfsIA7+i7FZbqXAIoHxc0JWqgZSAl2xocCWQEGxuzOteMSey3GPVvreEuAe9wV2ky8GAVFKUmho49WouTSu9XU705qio3XjXm6R5ZwWxK/XFmOczN2ipUkd76ZonHmS35FaGhjNblf0djFdsf98bxirbyQE=</D></RSAKeyValue>";
				
				//Create a UnicodeEncoder to convert between byte array and string.
				ByteConverter = new UnicodeEncoding();

				//Create byte arrays to hold original and encrypted data.
				byte[] dataToEncrypt = ByteConverter.GetBytes(strDataToEncrypt);
				byte[] encryptedData;
	            
				//Create a new instance of RSACryptoServiceProvider
				//using MachineStore
				CSPParam = new CspParameters();
				CSPParam.Flags = CspProviderFlags.UseMachineKeyStore;
				RSA = new RSACryptoServiceProvider(CSPParam);		
				RSA.FromXmlString(strXmlPublicKeyInfo);

				//Pass the data to ENCRYPT
				encryptedData = RSA.Encrypt(dataToEncrypt,false);	
				
				//Converto to string
				strEncryptedData = Convert.ToBase64String(encryptedData);
				
			}
			catch(Exception ex)
			{				
				throw(ex);
			}
			finally
			{					
				ByteConverter = null;
				RSA = null;
			}

			return strEncryptedData;
		}

		/// <summary>
		/// somehitng
		/// </summary>
		public static string DecryptPassword(string strDataToDecrypt)
		{
			//define objects
			CspParameters CSPParam;
			RSACryptoServiceProvider RSA;
			string strDecryptedData;
			UnicodeEncoding ByteConverter;

			try
			{	
				//Create a UnicodeEncoder to convert between byte array and string.
				ByteConverter = new UnicodeEncoding();

				//Create byte arrays to hold original and encrypted data.
				byte[] decryptedData;
	            
				//Create a new instance of RSACryptoServiceProvider
				CSPParam = new CspParameters();
				CSPParam.Flags = CspProviderFlags.UseMachineKeyStore;
				RSA = new RSACryptoServiceProvider(CSPParam);
				RSA.FromXmlString("<RSAKeyValue><Modulus>xBD7dzFwBQlbYnivkNWP058VPrYouYvVpT/PhSjZzYwPXjV3G/G8zSJ9W07z5U/9ltlpmoD8E8oTftdwBLzcliQLoN9wEYHyEFK83vS2GFs4nNbYFsXmIEQv9nCi7pWdmqShLVcZyIMRoRmx5xvxYXbzZfuMsBJp+24ddIOSkQE=</Modulus><Exponent>AQAB</Exponent><P>4FGK7v1qDDoGFE6A+zAhkhu78qJoQJbjuHosLHpWQOsklrkDS/53oAeymI7U5TpSIW5m2kn/Njnjd0UDWJaI8Q==</P><Q>38H0L2pGOUoOMcZQj7gN3ipufs3yz+qhxtO616UgcJRzwVX30f/4jHn0AN/Qsd0s/JfA47AU/+/KBwFv+vMJEQ==</Q><DP>nn6/LfH8VjtBRGPbmp/9eGEEQYIjK0/yAszAHYUrIJ+EsMJ04+Me3wHRXR2+q2Cz209IeQBDxSrN393xaUyj4Q==</DP><DQ>bu5uTA+JDx/614x1JjtejLVGoJEj/xZY7ATOTdjss0O1+7TOLwuTQrm3UbOkeHmpsvTdSURjtNtBsqVwyd4xsQ==</DQ><InverseQ>LjYNBt78I0qw6PWWq6qPGorAnCLNYpmkMqniSMkR7kAB2xW7hK/D3Em/qPpY3VmttJCCWuqQ14Q4JgKWdkhPGg==</InverseQ><D>DfNDYSS0WrdiuCvsGfsIA7+i7FZbqXAIoHxc0JWqgZSAl2xocCWQEGxuzOteMSey3GPVvreEuAe9wV2ky8GAVFKUmho49WouTSu9XU705qio3XjXm6R5ZwWxK/XFmOczN2ipUkd76ZonHmS35FaGhjNblf0djFdsf98bxirbyQE=</D></RSAKeyValue>");
					
				//Pass the data to DECRYPT
				decryptedData = RSA.Decrypt(Convert.FromBase64String(strDataToDecrypt),false);	
				
				//Get the string from array
				strDecryptedData = ByteConverter.GetString(decryptedData);

			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				//dispose objects	
				ByteConverter = null;
				RSA = null;				
			}		

			return strDecryptedData;
		}
	}
}
