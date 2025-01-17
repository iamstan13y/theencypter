﻿using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptionDecryption
{
    public partial class EncryDecry : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        // string hash should not be null
        string hash = "";
        //Encryption function
        private void Encrypt_Click(object sender, EventArgs e)
        {
        try{
            byte[] data = UTF8Encoding.UTF8.GetBytes(valuetxt.Text);
            using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider() )
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using(TripleDESCryptoServiceProvider tripdes = new TripleDESCryptoServiceProvider() {  Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.ISO10126 })
                {
                    ICryptoTransform transform = tripdes.CreateEncryptor();
                    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                    encrypttxt.Text = Convert.ToBase64String(result, 0, result.Length);
                }
            }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Values should not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
        //Decryption function
        private void Decrypt_Click(object sender, EventArgs e)
        {
        try{
            byte[] data = Convert.FromBase64String(encrypttxt.Text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripdes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.ISO10126 })
                {
                    ICryptoTransform transform = tripdes.CreateDecryptor();
                    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                    decrypttxt.Text = UTF8Encoding.UTF8.GetString(result);
                }
            }
            }
            catch
            {
                    MessageBox.Show("Values should not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
                    
        }

        private void aboutinfo_Click(object sender, EventArgs e)
        {
            about ab = new about();
            ab.ShowDialog();
        }
    }
}
