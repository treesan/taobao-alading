﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AladingWeb.Controls
{
    public partial class UserListControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public object DataSource
        {
            get { return RepUser.DataSource; }
            set { RepUser.DataSource = value; }
        }
    }
}