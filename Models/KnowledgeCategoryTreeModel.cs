using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeachPlan;
namespace Models
{
    public class KnowledgeCategoryTreeModel:KnowledgeCategory
    {
        public KnowledgeCategoryTreeModel (KnowledgeCategory category)
        {

        }
        public IEnumerable<KnowledgeCategory> ChildCategorys
        {
            get;
            set;
        }
    }
}