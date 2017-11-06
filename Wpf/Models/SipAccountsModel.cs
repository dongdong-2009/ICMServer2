using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    class SipAccountsModel : CollectionModelBase<sipaccount>
    {
        public SipAccountsModel(IDataService<sipaccount> dataService) : base(dataService)
        {
        }

        public override void Insert(sipaccount obj)
        {
            Random random = new Random();
            int randomCode = random.Next() % 10000;
            obj.C_password = GeneratePassword(randomCode);
            obj.C_randomcode = randomCode.ToString();
            obj.C_updatetime = DateTime.Now;
            obj.C_registerstatus = 0;
            obj.C_sync = 0;
            _dataService.Insert(obj);
            RefillDataAction.Defer(_refillDelay);
        }

        private string GeneratePassword(int randomCode)
        {
            Random random = new Random();
            string time = DateTime.Now.ToString() + randomCode.ToString();
            MD5 md5 = MD5.Create();
            byte[] source = Encoding.Default.GetBytes(time);
            byte[] crypto = md5.ComputeHash(source);
            return Convert.ToBase64String(crypto).Substring(0, 10);
        }

        protected override Func<sipaccount, bool> IdentityPredicate(sipaccount obj)
        {
            return (d => d.C_user == obj.C_user);
        }
    }
}
