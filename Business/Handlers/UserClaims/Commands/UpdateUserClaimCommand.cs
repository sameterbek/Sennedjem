﻿using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserClaims.Commands
{
    public class UpdateUserClaimCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClaimId { get; set; }

        public class UpdateUserClaimCommandHandler : IRequestHandler<UpdateUserClaimCommand, IResult>
        {
            private readonly IUserClaimDal _userClaimDal;

            public UpdateUserClaimCommandHandler(IUserClaimDal userClaimDal)
            {
                _userClaimDal = userClaimDal;
            }

            public async Task<IResult> Handle(UpdateUserClaimCommand request, CancellationToken cancellationToken)
            {
                var userClaimToUpdate = new UserClaim
                {
                    ClaimId = request.ClaimId,
                    Id = request.Id,
                    UserId = request.UserId
                };
                _userClaimDal.Update(userClaimToUpdate);
                await _userClaimDal.SaveChangesAsync();
                return new SuccessResult(Messages.UserClaimUpdated);
            }
        }
    }
}
