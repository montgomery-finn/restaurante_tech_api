using System.Threading.Tasks;

namespace restaurante_tech_api.Services.Interfaces
{
    public interface ISaveFileFromBase64StringService
    {
        public Task<string> Execute(string base64File);
    }
}
