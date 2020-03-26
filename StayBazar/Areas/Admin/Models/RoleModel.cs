using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Areas.Admin.Models
{
    public class RoleModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Is required")]
        [StringLength(100)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
        public List<CLayer.RolePermission> Permissions { get; set; }
        public string PermissionIds { get; set; }

        public RoleModel()
        {
            PermissionIds = "";
            Permissions = new List<CLayer.RolePermission>();
            Name = "";
            Id = 0;
        }
    }
}