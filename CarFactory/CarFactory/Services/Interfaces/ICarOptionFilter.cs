using CarFactory.Models.BodyForms;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;

namespace CarFactory.Services.Interfaces;

public interface ICarOptionFilter
{
    List<IEngine> FilterEngines( IBodyForm bodyForm, List<IEngine> engines );
    List<IGearBox> FilterGearBoxes( IEngine engine, IBodyForm bodyForm, List<IGearBox> gearBoxes );
}