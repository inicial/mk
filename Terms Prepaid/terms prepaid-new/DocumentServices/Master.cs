namespace DocumentServices
{
    partial class tbl_DogovorList
    {
        public override string ToString()
        {
            return this.DL_NAME;
        }
    }

    partial class Lanta_DictDocSpr
    {
        public override string ToString()
        {
            return this.ddgName;
        }
    }

    partial class tbl_Turist
    {
        public override string ToString()
        {
            string fullName = "";
            if (string.IsNullOrEmpty(this.TU_NAMELAT))
            {
                fullName += this.TU_NAMERUS + " " + this.TU_FNAMERUS;
                if (!string.IsNullOrEmpty(this.TU_SNAMERUS))
                {
                    fullName +=" "+ this.TU_SNAMERUS;
                }
            }
            else
            {
                fullName += this.TU_NAMELAT + " " + this.TU_FNAMELAT;
                if (!string.IsNullOrEmpty(this.TU_SNAMELAT))
                {
                    fullName += " " + this.TU_SNAMELAT;
                }
            }

            return fullName;
        }
    }

    partial class Tours
    {
        public override string ToString()
        {
            return this.DL_NAME;
        }
    }
}
