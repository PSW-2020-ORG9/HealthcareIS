// File:    Request.cs
// Author:  Lana
// Created: 27 May 2020 20:17:42
// Purpose: Definition of Class Request

using Model.Users.UserAccounts;
using System;

namespace Model.Requests
{
    public abstract class Request : Repository.Generics.Entity<int>
    {
        protected RequestStatus status;
        protected DateTime creationDate;
        protected DateTime reviewDate;
        protected String reviewerComment;
        protected EmployeeAccount sender;
        protected EmployeeAccount reviewer;
        protected int id;

        public RequestStatus Status { get => status; set => status = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public DateTime ReviewDate { get => reviewDate; set => reviewDate = value; }
        public string ReviewerComment { get => reviewerComment; set => reviewerComment = value; }
        public EmployeeAccount Sender { get => sender; set => sender = value; }
        public EmployeeAccount Reviewer { get => reviewer; set => reviewer = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is Request request &&
                   id == request.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }
    }
}