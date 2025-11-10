import React, { useState } from 'react';
import Overlay from "../components/overlay.jsx";
import "../../style/transparencia.css";

// Dados dos relatórios
const DADOS_RELATORIOS = [
  {
    id: 1,
    ano: 2025,
    corTema: 'laranja',
    titulo: 'Relatórios de 2025',
    texto: 'Descubra nossas conquistas de 2025. Obrigado por nos acompanhar!',
    imagem: 'image_fb20d9.png', 
    linkDownload: '#placeholder-pdf-2025',
  },
  {
    id: 2,
    ano: 2024,
    corTema: 'azul',
    titulo: 'Relatórios de 2024 (1º Semestre)',
    texto: 'Veja o impacto que causamos no primeiro semestre de 2024. Sua contribuição faz a diferença!',
    imagem: 'image_fb20d9.png', 
    linkDownload: '#placeholder-pdf-2024-1',
  },
  {
    id: 3,
    ano: 2024,
    corTema: 'verde-escuro',
    titulo: 'Relatórios de 2024 (2º Semestre)',
    texto: 'O resumo completo das nossas atividades e finanças do segundo semestre de 2024. Transparência total.',
    imagem: 'image_fb20d9.png', 
    linkDownload: '#placeholder-pdf-2024-2',
  },
];

// Componente de cada item de relatório (acordeão)
const RelatorioItem = ({ relatorio }) => {
  const [isOpen, setIsOpen] = useState(false);
  const corClass = `relatorio-item--${relatorio.corTema}`;

  return (
    <div className={`relatorio-item ${corClass}`}>
      {/* Cabeçalho clicável */}
      <div 
        className="relatorio-header" 
        onClick={() => setIsOpen(!isOpen)}
        aria-expanded={isOpen}
      >
        <h2 className="relatorio-titulo">{relatorio.titulo}</h2>
        <span className={`relatorio-icone ${isOpen ? 'open' : ''}`}>&#x25BC;</span>
      </div>

      {/* Corpo do acordeão */}
      <div className={`relatorio-body ${isOpen ? 'open' : ''}`}>
        <div className="relatorio-conteudo">
          <div className="relatorio-texto">
            <p>{relatorio.texto}</p>
            <p>Clique abaixo para acessar o relatório de {relatorio.ano}:</p>
            <a href={relatorio.linkDownload} className="btn-download" download>
              Baixar Relatório
            </a>
          </div>
          <div className="relatorio-imagem">
            <img 
              src={`/caminho/para/suas/imagens/${relatorio.imagem}`} 
              alt={`Pessoas no relatório de ${relatorio.ano}`} 
            />
          </div>
        </div>
      </div>
    </div>
  );
};

// Componente principal da página de Transparência
const Transparencia = () => {
  return (
    <div className="pagina-transparencia">
      {/* Banner principal */}
      <Overlay className="banner-transparencia">
        <h1 className="banner-texto">
          COMPROMISSO COM A <span className="destaque">TRANSPARÊNCIA</span>
        </h1>
        <p className="banner-subtexto">
          Confira nossos relatórios e resultados de forma simples e aberta.
        </p>
      </Overlay>

      {/* Filtros (placeholder, pode ser funcional futuramente) */}
      <div className="filtros">
        <div className="filtro-item">Todos os anos</div>
        <div className="filtro-item">Todos os tipos</div>
      </div>

      {/* Lista de relatórios em acordeão */}
      <div className="relatorios-acordeao">
        {DADOS_RELATORIOS.map(relatorio => (
          <RelatorioItem key={relatorio.id} relatorio={relatorio} />
        ))}
      </div>
    </div>
  );
};

export default Transparencia;
