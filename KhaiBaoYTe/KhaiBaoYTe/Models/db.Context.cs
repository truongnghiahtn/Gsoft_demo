﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KhaiBaoYTe.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KhaiBaoYTeEntities : DbContext
    {
        public KhaiBaoYTeEntities()
            : base("name=KhaiBaoYTeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CauHoi> CauHois { get; set; }
        public virtual DbSet<CauTraLoi> CauTraLois { get; set; }
        public virtual DbSet<CauTraLoi_ChiTiet> CauTraLoi_ChiTiet { get; set; }
        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<LoaiCauHoi> LoaiCauHois { get; set; }
        public virtual DbSet<Sub_CauHoi> Sub_CauHoi { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<CauTraLoiDung> CauTraLoiDungs { get; set; }
    }
}
