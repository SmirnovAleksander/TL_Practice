using CarFactory.Models.BodyForms;
using CarFactory.Models.Engines;
using CarFactory.Models.Enums;
using CarFactory.Models.GearBoxes;

namespace CarFactory.Services.Extensions;

public static class CarOptionExtensions
{
    public static List<IEngine> FilterByBodyForm( this List<IEngine> engines, IBodyForm bodyForm )
    {
        return engines.FindAll( e =>
            !( e.FuelType == FuelType.Electric && bodyForm is Pickup ) &&
            !( bodyForm.WeightKg > 2000 && e.Power < 180 )
        );
    }

    public static List<IGearBox> FilterByEngineAndBody( this List<IGearBox> gearBoxes, IEngine engine, IBodyForm bodyForm )
    {
        return gearBoxes.FindAll( g =>
        {
            if ( engine.FuelType == FuelType.Electric && g.TransmissionType != TransmissionType.ReductionGear )
                return false;

            if ( g.TransmissionType == TransmissionType.Cvt )
            {
                if ( bodyForm is Coupe || bodyForm is Pickup )
                    return false;

                if ( engine.Power > 200 )
                    return false;
            }

            return true;
        } );
    }
}
