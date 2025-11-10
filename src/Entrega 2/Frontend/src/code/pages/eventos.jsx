import React, { useState, useEffect } from 'react';
import axios from 'axios';
import "../../style/eventos.css";
import Overlay from "../components/overlay.jsx";

// Componente principal da página de eventos
const Eventos = () => {
  // Estado para armazenar a lista de eventos do backend
  const [eventos, setEventos] = useState([]);
  // Estado para controlar qual aba está ativa (próximos ou realizados)
  const [abaAtiva, setAbaAtiva] = useState('proximos');
  // Estado para armazenar posts simulados das redes sociais
  const [socialPosts, setSocialPosts] = useState([]);
  // Estado para controlar qual evento está selecionado no popup
  const [eventoSelecionado, setEventoSelecionado] = useState(null);

  // URL da API de eventos
  const EVENTOS_API_URL = 'http://localhost:5000/api/evento/get/eventos';

  // Busca os eventos do backend quando o componente é montado
  useEffect(() => {
    const fetchEventos = async () => {
      try {
        const response = await axios.get(EVENTOS_API_URL);
        setEventos(response.data);
      } catch (error) {
        console.error("Erro ao buscar eventos:", error);
      }
    };
    fetchEventos();
  }, []);

  // Links simulados de posts nas redes sociais
  const INSTAGRAM_POSTS = [
    "https://www.instagram.com/p/C8ZxqzFux6I",
    "https://www.instagram.com/p/POST_ID_2/",
    "https://www.instagram.com/p/POST_ID_3/",
  ];

  const FACEBOOK_POSTS = [
    "https://www.facebook.com/POST_LINK_1",
    "https://www.facebook.com/POST_LINK_2",
    "https://www.facebook.com/POST_LINK_3/",
  ];

  // Cria posts simulados com imagens e ícones para exibição
  useEffect(() => {
    const posts = [
      ...INSTAGRAM_POSTS.map((link, i) => ({
        id: `insta-${i}`,
        rede: "instagram",
        link,
        mediaUrl: `https://placehold.co/300x300/ffd5df/ffffff?text=Instagram+${i + 1}`,
        titulo: "Publicação no Instagram",
      })),
      ...FACEBOOK_POSTS.map((link, i) => ({
        id: `fb-${i}`,
        rede: "facebook",
        link,
        mediaUrl: `https://placehold.co/300x300/c9e4ff/000000?text=Facebook+${i + 1}`,
        titulo: "Publicação no Facebook",
      })),
    ];
    setSocialPosts(posts);
  }, []);

  // Função para gerar objeto Date completo a partir de data e horário
  const getFullDateTime = (dataEvento, horario) => {
    const datePart = dataEvento.split('T')[0];
    const fullDateTimeString = `${datePart}T${horario}`;
    return new Date(fullDateTimeString);
  };

  // Verifica se o evento já passou
  const isEventoPassado = (dataEvento, horario) => {
    const dataCompleta = getFullDateTime(dataEvento, horario);
    return dataCompleta < new Date();
  };

  // Filtra os eventos com base na aba ativa
  const eventosFiltrados = eventos.filter(evento => {
    const isPassado = isEventoPassado(evento.dataEvento, evento.horario);
    return abaAtiva === 'realizados' ? isPassado : !isPassado;
  });

  // Card individual de cada evento
  const EventoCard = ({ evento }) => {
    const { titulo, dataEvento, horario, local } = evento;
    const dataCompleta = getFullDateTime(dataEvento, horario);
    const dia = dataCompleta.getDate();
    const mes = dataCompleta.toLocaleDateString('pt-BR', { month: 'short' }).replace('.', '');
    const realizado = isEventoPassado(dataEvento, horario);

    return (
      <div
        className={`evento-card ${realizado ? 'realizado-card' : 'proximo-card'}`}
        onClick={() => setEventoSelecionado(evento)}
      >
        {/* Imagem do evento com data sobreposta */}
        <Overlay className="evento-imagem">
          <div className="evento-data">
            <span className="dia">{dia}</span>
            <span className="mes">{mes}</span>
          </div>
        </Overlay>

        {/* Informações do evento */}
        <div className="evento-info">
          <p className="nome-evento">{titulo.toUpperCase()}</p>
          <p className="local-evento">Local: {local}</p>
          <div className="evento-icones-sociais">
            <i className="fas fa-share-alt" title="Compartilhar"></i>
          </div>
        </div>
      </div>
    );
  };

  return (
    <div className="pagina-eventos">
      {/* Cabeçalho com Overlay */}
      <div className="header-eventos">
        <Overlay className="header-overlay">
          <h1 className="header-titulo">NOSSOS EVENTOS</h1>
          <p className="header-subtitulo">
            Veja o que está por vir e participe da nossa programação.
          </p>
        </Overlay>
      </div>

      {/* Seção de eventos com abas */}
      <div className="eventos-secao">
        <div className="abas-container">
          <button
            className={`aba-botao ${abaAtiva === 'realizados' ? 'ativa' : ''}`}
            onClick={() => setAbaAtiva('realizados')}
          >
            Realizados
          </button>
          <button
            className={`aba-botao ${abaAtiva === 'proximos' ? 'ativa' : ''}`}
            onClick={() => setAbaAtiva('proximos')}
          >
            Próximos
          </button>
        </div>

        <h2 className="secao-titulo">Encontros e ações que inspiram mudanças</h2>

        {/* Lista de cards de eventos */}
        <div className="cards-eventos-container">
          {eventosFiltrados.length > 0 ? (
            eventosFiltrados
              .sort((a, b) => getFullDateTime(a.dataEvento, a.horario) - getFullDateTime(b.dataEvento, b.horario))
              .map((evento) => (
                <EventoCard key={evento.id} evento={evento} />
              ))
          ) : (
            <p className="mensagem-vazio">
              Nenhum evento {abaAtiva === 'realizados' ? 'realizado' : 'próximo'} encontrado.
            </p>
          )}
        </div>
      </div>

      {/* Seção de redes sociais com posts simulados */}
      <div className="social-secao">
        <h2 className="social-titulo">siga-nos nas redes sociais</h2>
        <div className="social-grid">
          {socialPosts.map(post => (
            <a
              key={post.id}
              href={post.link}
              target="_blank"
              rel="noopener noreferrer"
              className="social-post-item"
              style={{ backgroundImage: `url(${post.mediaUrl})` }}
            >
              <div className="social-post-overlay">
                <i className={`fab fa-${post.rede}`}></i>
                <span>{post.titulo}</span>
              </div>
            </a>
          ))}
        </div>
      </div>

      {/* Popup com detalhes do evento selecionado */}
      {eventoSelecionado && (
        <div className="popup-overlay" onClick={() => setEventoSelecionado(null)}>
          <div className="popup-container" onClick={(e) => e.stopPropagation()}>
            <button className="popup-fechar" onClick={() => setEventoSelecionado(null)}>×</button>
            <img
              src="https://placehold.co/800x400/d4afb9/ffffff?text=Imagem+do+Evento"
              alt={eventoSelecionado.titulo}
              className="popup-imagem"
            />
            <div className="popup-conteudo">
              <h2>{eventoSelecionado.titulo}</h2>
              <p><strong>Data:</strong> {new Date(eventoSelecionado.dataEvento).toLocaleDateString()}</p>
              <p><strong>Horário:</strong> {eventoSelecionado.horario}</p>
              <p><strong>Local:</strong> {eventoSelecionado.local}</p>
              <p className="popup-descricao">{eventoSelecionado.descricao}</p>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default Eventos;
