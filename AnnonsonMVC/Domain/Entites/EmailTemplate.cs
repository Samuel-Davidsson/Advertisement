using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class EmailTemplate
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateSubject { get; set; }
        public string TemplateBody { get; set; }
    }
}
