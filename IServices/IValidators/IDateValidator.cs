using System.Runtime.InteropServices.JavaScript;
using IServices.IAppointments;
using IServices.IValidtors.IAppointments;

namespace IServices.IValidators;

public interface IDateValidator
{
    public    void  Validate(DateTime date);
}