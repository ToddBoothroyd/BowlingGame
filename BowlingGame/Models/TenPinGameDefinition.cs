using AutoMapper;
using System.IO;
using System.Text.Json;

namespace ToddBoothroyd_BowlingGame
{
    /// <summary>
    /// Populate the base rules for a ten pin bowling game.
    /// 
    /// For the ten-pin game data, the information is retrieved from a Json file and mapped to a temporary model.
    /// The json file, for example, comes from a third-party.
    /// Since the data does not match the internal library, a third-party utility library is used to map the data.
    /// </summary>
    internal sealed class TenPinGameDefinition : GameData, IBowlingGameData
    {
        private IMapper _iMapper;

        internal TenPinGameDefinition()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TenPinGameCustomerDefinition, BowlingGameDefinition>()
                .ForMember(dto => dto.Name, map => map.MapFrom(source => source.GameName))
                .ForMember(dto => dto.LastFrameNumberOfRolls, map => map.MapFrom(source => source.LastFrame))
                .ForMember(dto => dto.NumberOfRolls, map => map.MapFrom(source => source.Throws))
                .ForMember(dto => dto.PinCount, map => map.MapFrom(source => source.Pins))
                .ForMember(dto => dto.Rules, map => map.MapFrom(source => source.HowToPlay))
                .ForMember(dto => dto.Description, map => map.MapFrom(source => source.Details));
            });

            _iMapper = config.CreateMapper();
        }

        public override BowlingGameDefinition RetrieveBowlingDefinition()
        {
            string jsonString = File.ReadAllText(Directory.GetCurrentDirectory() + "\\data\\TenPinGameData.json");
            TenPinGameCustomerDefinition tenPinRawData = JsonSerializer.Deserialize<TenPinGameCustomerDefinition>(jsonString)!;
            return _iMapper.Map<BowlingGameDefinition>(tenPinRawData);
        }
    }
}
