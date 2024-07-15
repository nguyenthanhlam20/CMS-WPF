using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinancialWPFApp.Models
{
    public partial class FinancialManagementContext : DbContext
    {
        public FinancialManagementContext()
        {
        }

        public FinancialManagementContext(DbContextOptions<FinancialManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; } = null!;
        public virtual DbSet<TransactionType> TransactionTypes { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=LAPTOP-ALU23ESG;database=FinancialManagement;uid=sa;pwd=55555");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__Account__AB6E6165725A56AF");

                entity.ToTable("Account");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Avatar)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");  
                
                entity.Property(e => e.RegisteredDate)
                    .HasColumnType("date")
                    .HasColumnName("registered_date");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasColumnName("full_name");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Account__role_id__398D8EEE");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.FromTo)
                    .HasMaxLength(100)
                    .HasColumnName("from_to");

                entity.Property(e => e.Owner)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("owner");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("date")
                    .HasColumnName("transaction_date");

                entity.Property(e => e.TransactionStatusId).HasColumnName("transaction_status_id");

                entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");

                entity.Property(e => e.WalletId).HasColumnName("wallet_id");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("FK__Transacti__owner__48CFD27E");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.TransactionStatusId)
                    .HasConstraintName("FK__Transacti__trans__4AB81AF0");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .HasConstraintName("FK__Transacti__trans__49C3F6B7");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.WalletId)
                    .HasConstraintName("FK__Transacti__walle__4BAC3F29");
            });

            modelBuilder.Entity<TransactionStatus>(entity =>
            {
                entity.ToTable("Transaction_Status");

                entity.Property(e => e.TransactionStatusId).HasColumnName("transaction_status_id");

                entity.Property(e => e.TransactionStatusName)
                    .HasMaxLength(100)
                    .HasColumnName("transaction_status_name");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("Transaction_Type");

                entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");

                entity.Property(e => e.TransactionTypeName)
                    .HasMaxLength(100)
                    .HasColumnName("transaction_type_name");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.Property(e => e.WalletId).HasColumnName("wallet_id");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.WalletName)
                    .HasMaxLength(100)
                    .HasColumnName("wallet_name");

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.Email)
                    .HasConstraintName("FK__Wallet__email__45F365D3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
