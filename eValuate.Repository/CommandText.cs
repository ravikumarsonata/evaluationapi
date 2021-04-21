using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace eValuate.Repository
{
    public class CommandText : ICommandText
    {
        public string GetProducts => "Select * From Products";
        public string GetProductById => "Select * From Products Where Id= @Id";
        public string AddProduct => "Insert Into  Products (Name, Cost, CreatedDate) Values (@Name, @Cost, @CreatedDate)";
        public string UpdateProduct => "Update Products set Name = @Name, Cost = @Cost, CreatedDate = GETDATE() Where Id =@Id";
        public string RemoveProduct => "Delete From Products Where Id= @Id";

        // GBW Database Server
        public string GetUsers => "Select * From Test_User";
        public string AddFFQTemplate => @"insert into ffq_layout (qnr_ref, rest)
                                        values(@QnrRef, (select replace(b.rest, @CopyFromStr, @QnrRefStr) 
                                        from ffq_layout b where b.qnr_ref = @CopyFrom))";
        public string AddMOTLayout => @"insert into mot_layout (qnr_ref, rest)
                                        values(@QnrRef, (select replace(b.rest, @CopyFromStr, @QnrRefStr) 
                                        from mot_layout b where b.qnr_ref = @CopyFrom))";
    }
}
