using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.ChaHub
{
  public class LoopyHub : Hub
  {
    IUnitOfWork _uow;
    IMapper _mapper;

    public LoopyHub(IUnitOfWork uow, IMapper mapper)
    {
      this._uow = uow;
      this._mapper = mapper;
    }
    public async Task<object> Send(LoggerDetailsModel data)
    {
      if (data != null)
      {
        LoggerDetails obj = new LoggerDetails();
        //LoggerDetails obj = _mapper.Map<LoggerDetails>(data);
        obj.CreatedById = data.UserId;
        obj.CreatedDate = DateTime.Now;
        obj.NotificationId = data.NotificationId;
        obj.IsRead = data.IsRead;
        obj.UserName = data.UserName;
        obj.LoggedDetail = data.LoggedDetail;
        //obj.NotificationPath = data.NotificationPath;
        await _uow.LoggerDetailsRepository.AddAsyn(obj);
        await _uow.SaveAsync();
      }

      var list = await _uow.LoggerDetailsRepository.FindAllAsync(x=>x.CreatedById == data.UserId);
      list = list.OrderByDescending(x=>x.CreatedDate).ToList();
      //var unReadCounts = list.Count(x=>x.IsRead == false);
      //LogData model = new LogData();
      //model.LogList = list;
      //model.unReadCounts = unReadCounts;
      return Clients.All.SendAsync("Send", list);
    }
  }
}
