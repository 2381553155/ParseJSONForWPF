using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseJSONForWPF
{
    public class TextResponse
    {
        /// <summary>
        /// 状态码
        /// </summary>
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// 文字内容
        /// </summary>
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
