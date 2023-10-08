using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Results
{
    public class ResultDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class ResultDTO<T> : ResultDTO
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
