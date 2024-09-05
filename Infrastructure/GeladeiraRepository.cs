using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GeladeiraRepository
    {
        private GeladeiraContext _contexto;

        public GeladeiraRepository(GeladeiraContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<ItemModel>> ObterItensAsync()
        {
            return await _contexto.Itens.ToListAsync();
        }

        public async Task<ItemModel> ObterItemPorIdAsync(int id)
        {
            var item = await _contexto.Itens.FindAsync(id);
            if (item != null)
            {
                return item;
            }
            return null;
        }

        public async Task<ItemModel> AdicionarItemAsync(ItemModel item)
        {
            _contexto.Itens.Add(item);
            await _contexto.SaveChangesAsync();

            return item;
        }

        public async Task<ItemModel> AtualizarItemAsync(ItemModel item)
        {
            var verificarItem = await ObterItemPorIdAsync(item.Id);
            if (verificarItem == null)
            {
                return null;
            }

            _contexto.Entry(verificarItem).CurrentValues.SetValues(item);
            await _contexto.SaveChangesAsync();
            return item;
        }

        public async Task RemoverItemAsync(int id)
        {
            var item = await ObterItemPorIdAsync(id);

            if (item != null)
            {
                _contexto.Itens.Remove(item);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}