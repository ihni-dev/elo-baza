//using System;

//namespace EloBaza.Domain
//{
//    public class Answer
//    {
//        public Guid Id { get; private set; }
//        public string Content { get; private set; }
//        public bool IsValid { get; private set; }

//        public Answer(Guid id, string content, bool isValid)
//        {
//            if (string.IsNullOrWhiteSpace(content))
//                throw new ArgumentException("Answer content must have a value");

//            Id = id;
//            Content = content;
//            IsValid = isValid;
//        }
//    }
//}