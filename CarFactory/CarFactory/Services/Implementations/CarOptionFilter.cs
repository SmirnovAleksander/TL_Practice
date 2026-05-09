using CarFactory.Models.BodyForms;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Services.Interfaces;

namespace CarFactory.Services.Implementations;

public class CarOptionFilter : ICarOptionFilter
{
    public List<IEngine> FilterEngines( IBodyForm bodyForm, List<IEngine> engines )
    {
        List<IEngine> result = new List<IEngine>();

        foreach ( IEngine engine in engines )
        {
            if ( engine.FuelType == "electric" && bodyForm is Pickup )
            {
                continue;
            }

            if ( bodyForm.Weight > 2000 && engine.Power < 180 )
            {
                continue;
            }

            result.Add( engine );
        }

        return result;
    }

    public List<IGearBox> FilterGearBoxes( IEngine engine, IBodyForm bodyForm, List<IGearBox> gearBoxes )
    {
        List<IGearBox> result = new List<IGearBox>();

        foreach ( IGearBox gearBox in gearBoxes )
        {
            if ( engine.FuelType == "electric" )
            {
                if ( gearBox.TransmissionType != "single" )
                {
                    continue;
                }
            }

            if ( gearBox.TransmissionType == "cvt" )
            {
                if ( bodyForm is Coupe || bodyForm is Pickup )
                {
                    continue;
                }

                if ( engine.Power > 200 )
                {
                    continue;
                }
            }

            result.Add( gearBox );
        }

        return result;
    }
}