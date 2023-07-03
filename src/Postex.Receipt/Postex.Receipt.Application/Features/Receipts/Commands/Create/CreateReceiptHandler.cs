using AutoMapper;
using MediatR;
using Postex.receipt.Application.Dtos;
using Postex.receipt.Domain.Models;
using Postex.Receipt.Application;
using Postex.Receipt.Application.Service;
using Postex.SharedKernel.Interfaces;

namespace Postex.receipt.Application.Features
{
    public class CreateReceiptHandler : IRequestHandler<CreateReceiptCommand, string>
    {



        public CreateReceiptHandler()
        {


        }



        async Task<string> IRequestHandler<CreateReceiptCommand, string>.Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
        {
            var rep = new CreateReport();
            rep.Create();
            return rep.ToString();
        }
    }
}
