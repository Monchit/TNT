﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TncNokTooling.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TncNokToolingEntities : DbContext
    {
        public TncNokToolingEntities()
            : base("name=TncNokToolingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<td_files> td_files { get; set; }
        public DbSet<td_pr> td_pr { get; set; }
        public DbSet<td_tooling> td_tooling { get; set; }
        public DbSet<td_tran> td_tran { get; set; }
        public DbSet<tm_action> tm_action { get; set; }
        public DbSet<tm_control_route> tm_control_route { get; set; }
        public DbSet<tm_file_type> tm_file_type { get; set; }
        public DbSet<tm_leadtime> tm_leadtime { get; set; }
        public DbSet<tm_nok_plant> tm_nok_plant { get; set; }
        public DbSet<tm_process> tm_process { get; set; }
        public DbSet<tm_product> tm_product { get; set; }
        public DbSet<tm_status> tm_status { get; set; }
        public DbSet<tm_type> tm_type { get; set; }
        public DbSet<tm_user> tm_user { get; set; }
        public DbSet<tm_user_lv> tm_user_lv { get; set; }
        public DbSet<tm_user_type> tm_user_type { get; set; }
        public DbSet<v_tran> v_tran { get; set; }
        public DbSet<v_sys_user> v_sys_user { get; set; }
        public DbSet<v_tran_show> v_tran_show { get; set; }
        public DbSet<tm_user_nok> tm_user_nok { get; set; }
        public DbSet<td_eta_tnc> td_eta_tnc { get; set; }
        public DbSet<tm_site> tm_site { get; set; }
        public DbSet<td_import> td_import { get; set; }
        public DbSet<v_issue_group> v_issue_group { get; set; }
    }
}
