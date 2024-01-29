using AutoMapper;

namespace Notifications.Application.Common.Mappings;

public interface IMapFrom<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}