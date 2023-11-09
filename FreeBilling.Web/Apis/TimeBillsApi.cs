using FluentValidation;
using FreeBilling.Data.Entities;
using FreeBilling.Web.Data;
using FreeBilling.Web.Models;
using Mapster;

namespace FreeBilling.Web.Apis
{
    public static class TimeBillsApi
    {
        public static void Register(WebApplication app)
        {
            app.MapGet("/api/timebills/{id:int}", GetTimeBill)
                .WithName("GetTimeBill");

            app.MapPost("api/timebills", PostTimeBill);
        }


        public static async Task<IResult> GetTimeBill(IBillingRepository repository, int id)
        {
            

            var bill = await repository.GetTimeBill(id);

            if (bill is null) Results.NotFound();

            return Results.Ok(bill);
        }

        public static async Task<IResult> PostTimeBill(IBillingRepository repository, 
            IValidator<TimeBillModel> validator,
            TimeBillModel model)
        {
            var validation = validator.Validate(model);

            if(!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }

            var newEntity = model.Adapt<TimeBill>();
            //var newEntity = new TimeBill()
            //{
             //   CustomerId = model.CustomerId,
              //  EmployeeId = model.EmployeeId,
                //Hours = model.HoursWorked,
                //BillingRate = model.Rate,
                //Date = model.Date,
                //WorkPerformed = model.Work
            //};

            repository.AddEntity(newEntity);
            if (await repository.SaveChanges())
            {
                var newBill = await repository.GetTimeBill(newEntity.Id);
                return Results.CreatedAtRoute("GetTimeBill", new { id = newEntity.Id }, newBill);
            }
            else
            {
                return Results.BadRequest();
            }
        }
    }
}
