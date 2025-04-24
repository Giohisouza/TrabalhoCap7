namespace Gestao.Residuos.ViewModel.Residuo
{
    public class ResiduoPaginacaoViewModel
    {
        public IEnumerable<ResiduoViewModel> Caminhao { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => Caminhao.Count() == PageSize;
        public string PreviousPageUrl => HasPreviousPage ? $"/Cliente?pagina={CurrentPage - 1}&tamanho={PageSize}" : "";
        public string NextPageUrl => HasNextPage ? $"/Cliente?pagina={CurrentPage + 1}&tamanho={PageSize}" : "";
    }
}
