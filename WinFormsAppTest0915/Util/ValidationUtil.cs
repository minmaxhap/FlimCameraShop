using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppTest0915
{
    class ValidationUtil
    {
        public void NameValid(string name)
        {
            //이름 유효성 검사 : 한글이랑 알파벳 소문자, 대문자만. 2자리 이상 30자리 이하
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("필수 입력입니다.");
                return;
            }
            if (!(CommonUtil.NameCheck(name)))
            {
                MessageBox.Show("유효한 이름을 입력해주세요.");
                return;
            }
            if (name.Length < 2 || name.Length > 30)
            {
                MessageBox.Show("이름은 2자리 이상 30자리 이하여야 합니다.");
                return;
            }
        }
       public void PhoneValid(string phone1, string phone2, string phone3)
        {
            if (string.IsNullOrWhiteSpace(phone1))
            {
                MessageBox.Show("휴대전화 앞자리를 다시 한번 선택해주세요.");
                return;
            }
            if (string.IsNullOrWhiteSpace(phone2))
            {
                MessageBox.Show("필수 입력입니다.");
                return;
            }
            if (string.IsNullOrWhiteSpace(phone3))
            {
                MessageBox.Show("필수 입력입니다.");
                return;
            }
        }
    }
}
