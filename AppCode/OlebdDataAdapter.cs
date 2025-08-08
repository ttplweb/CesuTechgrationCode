using System;
using System.Data;
using System.Data.OleDb;

namespace TechGration.AppCode
{
    internal class OlebdDataAdapter
    {
        private OleDbCommand com;

        public OlebdDataAdapter(OleDbCommand com)
        {
            this.com = com;
        }

        internal void Fill(DataTable dt)
        {
            throw new NotImplementedException();
        }
    }
}