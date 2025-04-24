namespace Gestao.Residuos.ViewModel.Morador
{
    public class MoradorPaginacaoViewModel
    {
        public IEnumerable<MoradorViewModel> Morador { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => Morador.Count() == PageSize;
        public string PreviousPageUrl => HasPreviousPage ? $"/Cliente?pagina={CurrentPage - 1}&tamanho={PageSize}" : "";
        public string NextPageUrl => HasNextPage ? $"/Cliente?pagina={CurrentPage + 1}&tamanho={PageSize}" : "";
    }
}
