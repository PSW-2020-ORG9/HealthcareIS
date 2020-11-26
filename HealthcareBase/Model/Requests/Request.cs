// File:    Request.cs
// Author:  Lana
// Created: 27 May 2020 20:17:42
// Purpose: Definition of Class Request

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Requests
{
    public abstract class Request : IEntity<int>
    {
        protected DateTime creationDate;
        protected int id;
        protected DateTime reviewDate;
        protected AdministrationAccount reviewer;
        protected string reviewerComment;
        protected AdministrationAccount sender;
        protected RequestStatus status;

        [Column(TypeName = "nvarchar(24)")]
        public RequestStatus Status
        {
            get => status;
            set => status = value;
        }

        public DateTime CreationDate
        {
            get => creationDate;
            set => creationDate = value;
        }

        public DateTime ReviewDate
        {
            get => reviewDate;
            set => reviewDate = value;
        }

        public string ReviewerComment
        {
            get => reviewerComment;
            set => reviewerComment = value;
        }

        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        public AdministrationAccount Sender
        {
            get => sender;
            set => sender = value;
        }

        [ForeignKey("Reviewer")]
        public int ReviewerId { get; set; }
        public AdministrationAccount Reviewer
        {
            get => reviewer;
            set => reviewer = value;
        }

        [Key]
        public int Id
        {
            get => id;
            set => id = value;
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Request request &&
                   id == request.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}