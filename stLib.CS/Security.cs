using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace stLib.CS.Security {
    public class SHA {
        private bool isReturnNum;
        public string SHA512Encrypt( string strIN ) {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA512 sha512 = new SHA512Managed();

            tmpByte = sha512.ComputeHash( GetKeyByteArray( strIN ) );
            sha512.Clear();

            return GetStringValue( tmpByte );

        }
        public string SHA1Encrypt( string strIN ) {
            byte[] tmpByte;
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            tmpByte = sha1.ComputeHash( GetKeyByteArray( strIN ) );
            sha1.Clear();

            return GetStringValue( tmpByte );

        }

        public string SHA256Encrypt( string strIN ) {
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();

            tmpByte = sha256.ComputeHash( GetKeyByteArray( strIN ) );
            sha256.Clear();

            return GetStringValue( tmpByte );

        }

        private string GetStringValue( byte[] Byte ) {
            string tmpString = "";
            if( this.isReturnNum == false ) {
                ASCIIEncoding Asc = new ASCIIEncoding();
                tmpString = Asc.GetString( Byte );
            } else {
                int iCounter;
                for( iCounter = 0; iCounter < Byte.Length; iCounter++ ) {
                    tmpString = tmpString + Byte[iCounter].ToString();
                }
            }
            return tmpString;
        }

        private byte[] GetKeyByteArray( string strKey ) {

            ASCIIEncoding Asc = new ASCIIEncoding();

            int tmpStrLen = strKey.Length;
            byte[] tmpByte = new byte[tmpStrLen - 1];

            tmpByte = Asc.GetBytes( strKey );

            return tmpByte;

        }
    }
}
