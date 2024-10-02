// using AutoMapper;
// using LoggingBestPract.Application.CQRS.Commands.WorkOrder;
// using LoggingBestPract.Application.CQRS.Commands.WorkOrder.ApproveWorkOrder;
// using LoggingBestPract.Application.CQRS.Commands.WorkOrder.CancelWorkOrder;
// using LoggingBestPract.Application.CQRS.Commands.WorkOrder.CheckWorkOrder;
// using LoggingBestPract.Application.CQRS.Commands.WorkOrder.DocumentWorkOrder;
// using LoggingBestPract.Application.CQRS.Commands.WorkOrder.ServiceWorkOrder;
// using LoggingBestPract.Application.CQRS.Commands.WorkOrder.SetupWorkOrder;
// using LoggingBestPract.Application.Features.Auth.Command.Login;
// using LoggingBestPract.Application.Models.Requests;
// using LoggingBestPract.Application.Models.Requests.Payser;
// using LoggingBestPract.Application.Models.Requests.WorkOrder;
// using LoggingBestPract.Domain.Entities;
//
// namespace LoggingBestPract.Application.Mappings;
//
// public class WorkOrderProfile : Profile
// {
//     public WorkOrderProfile()
//     {
//         CreateMap<WorkOrder, SetupWorkOrderCommandRequest>().ReverseMap();
//
//         CreateMap<MerchantCommand, MerchantRequest>().ReverseMap();
//         CreateMap<DeviceCommand, DeviceRequest>().ReverseMap();
//
//         CreateMap<LoginCommandRequest, WorkOrderRequest>().ReverseMap();
//         CreateMap<ServiceWorkOrderCommandRequest, WorkOrderRequest>().ReverseMap();
//         CreateMap<SetupWorkOrderCommandRequest, WorkOrderRequest>().ReverseMap();
//         CreateMap<DocumentWorkOrderCommandRequest, WorkOrderRequest>().ReverseMap();
//         CreateMap<CheckWorkOrderCommandRequest, WorkOrderRequest>().ReverseMap();
//         CreateMap<ApproveWorkOrderCommandRequest, WorkOrderRequest>().ReverseMap();
//         CreateMap<ApproveWorkOrderCommandRequest, CancelWorkOrderCommandRequest>().ReverseMap();
//
//         CreateMap<UpdateWorkOrderStatusCommandRequest, UpdateWorkOrderStatusRequest>().ReverseMap();
//         CreateMap<GetWorkOrderStatusQueryRequest, GetWorkOrderStatusRequest>().ReverseMap();
//     }
// }