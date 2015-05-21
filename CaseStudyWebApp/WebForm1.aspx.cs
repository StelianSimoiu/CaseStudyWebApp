using CaseStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CaseStudyWebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static GitHub Git = new GitHub();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           string[] userNames = this.GetArgs();
           
            if (userNames[0].Equals(String.Empty) && userNames[1].Equals(String.Empty))
            {
                this.Label1.Text = "Both fields are empty. Give at least one user name.";
                return;
            }
            

            if (!userNames[0].Equals(String.Empty) && !userNames[1].Equals(String.Empty))
            {
                 this.Label1.Text = Git.CaseTwo(userNames);
                 return;
            }

            if (!userNames[0].Equals(String.Empty))
            {
                 this.Label1.Text = Git.CaseOne(userNames[0]);
                 return;
            }
            if (!userNames[1].Equals(String.Empty))
            {
                this.Label1.Text = Git.CaseOne(userNames[1]);
                return;
            }
        }

        string[] GetArgs()
        {
            string[] ret = {"",""};

            ret[0] = this.TextBox1.Text;
            ret[1] = this.TextBox2.Text;

            return ret;
        }
    }
}