




using System;
namespace T4Demo.Entities
{
    public class Person
    {
        /// <summary>
        /// 
        /// </summary>      
        public Guid Oid { get; set; }
        /// <summary>
        /// 
        /// </summary>      
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>      
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>      
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>      
        public bool? Married { get; set; }
        /// <summary>
        /// 
        /// </summary>      
        public string SpouseName { get; set; }
        /// <summary>
        /// 
        /// </summary>      
        public int? OptimisticLockField { get; set; }
        /// <summary>
        /// 
        /// </summary>      
        public int? GCRecord { get; set; }
        /// <summary>
        /// 
        /// </summary>      
        public string MiddleName { get; set; }
        /// <summary>
        /// 
        /// </summary>      
        public string Email { get; set; }

    }
}


