import "../../style/atividades.css";
import React, { useState } from 'react';

import Overlay from "../components/overlay.jsx";
import seta_icon from "../../assets/seta_icon.png";

// Componente para mostrar o ícone da seta
const SetaIcon = () => (
  <img
    src={seta_icon} 
    alt="ícone de seta"
    className="icon-seta" 
  />
);

// Lista de atividades mockadas para exibir na página
const mockActivities = [
  {
    id: 1,
    title: "Projeto 'Sementes do Amanhã'",
    description: "Iniciativa de reflorestamento e educação ambiental com foco em comunidades rurais.",
    category: "Meio Ambiente",
    imageUrl: "https://placehold.co/600x400/10B981/FFFFFF?text=AÇÃO+01",
    date: "15/Out/2025"
  },
  {
    id: 2,
    title: "Oficina de Desenvolvimento Integral",
    description: "Série de workshops sobre habilidades socioemocionais para jovens em situação de vulnerabilidade.",
    category: "Desenvolvimento Humano",
    imageUrl: "https://placehold.co/600x400/3B82F6/FFFFFF?text=AÇÃO+02",
    date: "01/Set/2025"
  },
  {
    id: 3,
    title: "Campanha 'Laços Comunitários'",
    description: "Distribuição de cestas básicas e itens de higiene, fortalecendo a rede de apoio local.",
    category: "Fortalecimento de Laços",
    imageUrl: "https://placehold.co/600x400/F59E0B/FFFFFF?text=AÇÃO+03",
    date: "10/Ago/2025"
  },
  {
    id: 4,
    title: "Mutirão de Limpeza Urbana",
    description: "Ação voluntária de limpeza de rios e praças, promovendo a conscientização ecológica.",
    category: "Meio Ambiente",
    imageUrl: "https://placehold.co/600x400/EF4444/FFFFFF?text=AÇÃO+04",
    date: "05/Jul/2025"
  },
];

// Componente para renderizar cada card de atividade
const ActivityCard = ({ activity }) => {
  // Função para definir a classe CSS da categoria
  const getCategoryClass = (category) => {
    switch (category) {
      case "Meio Ambiente": return "category-meio-ambiente";
      case "Desenvolvimento Humano": return "category-desenvolvimento-humano";
      case "Fortalecimento de Laços": return "category-fortalecimento-lacos";
      default: return "category-default";
    }
  };

  return (
    <div className="activity-card">
      <div className="card-image-wrapper">
        <img
          src={activity.imageUrl}
          alt={activity.title}
          className="card-image"
          onError={(e) => { e.target.onerror = null; e.target.src = "https://placehold.co/600x400/E5E7EB/4B5563?text=Sem+Imagem"; }}
        />
      </div>
      <div className="card-content">
        <span className={`card-category ${getCategoryClass(activity.category)}`}>
          {activity.category}
        </span>
        <h3 className="card-title">{activity.title}</h3>
        <p className="card-description">{activity.description}</p>
        <div className="card-footer">
          <span>Publicado em: {activity.date}</span>
          <button className="card-details-button">
            Ver Detalhes &rarr;
          </button>
        </div>
      </div>
    </div>
  );
};

// Componente principal da página
const App = () => {
  // Estado para armazenar atividades
  const [activities, setActivities] = useState(mockActivities);
  // Estado para controlar a exibição do modal
  const [showModal, setShowModal] = useState(false);

  // Abrir modal para adicionar atividade (futuro CRUD)
  const handleAddActivity = () => {
    setShowModal(true);
  };

  // Fechar modal
  const closeModal = () => {
    setShowModal(false);
  };

  // Ícones SVG inline para os pilares
  const IconHeart = (props) => (
    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" {...props}>
      <path d="M19 14c1.49-1.46 3-3.21 3-5.5A5.5 5.5 0 0 0 16.5 3c-1.76 0-3 .5-4.5 2-1.5-1.5-2.74-2-4.5-2A5.5 5.5 0 0 0 2 8.5c0 2.3 1.5 4.05 3 5.5l7 7Z" />
    </svg>
  );

  const IconGlobe = (props) => (
    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" {...props}>
      <circle cx="12" cy="12" r="10" />
      <path d="M12 2a14.5 14.5 0 0 0 0 20 14.5 14.5 0 0 0 0-20" />
      <path d="M2 12h20" />
    </svg>
  );

  const IconZap = (props) => (
    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" {...props}>
      <path d="M13 2L3 14h9l-1 8 11-12h-9l1-8z" />
    </svg>
  );
  
  const IconChevronDown = (props) => (
    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" {...props}>
      <path d="m6 9 6 6 6-6" />
    </svg>
  );

  const IconPlus = (props) => (
    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" {...props}>
      <path d="M12 5v14" />
      <path d="M5 12h14" />
    </svg>
  );

  // Dados dos pilares de ação do instituto
  const Pillars = [
    { icon: <IconHeart style={{ width: '2rem', height: '2rem', color: 'white' }} />, title: "Fortalecimento de Laços", description: "Criação de redes de apoio e coesão social." },
    { icon: <IconGlobe style={{ width: '2rem', height: '2rem', color: 'white' }} />, title: "Proteção Ambiental", description: "Foco na sustentabilidade e preservação de biomas." },
    { icon: <IconZap style={{ width: '2rem', height: '2rem', color: 'white' }} />, title: "Desenvolvimento Humano Integral", description: "Promoção de educação e bem-estar físico e emocional." },
  ];

  // Função para rolar até a seção de atividades
  const scrollToActivities = () => {
    const activitiesSection = document.getElementById('activities-list');
    if (activitiesSection) {
      activitiesSection.scrollIntoView({ behavior: 'smooth' });
    }
  };
  
  return (
    <div className="min-h-screen">
      
      {/* SEÇÃO HERO: Cabeçalho principal da página */}
      <Overlay className="hero-section-mista">
        <div className="hero-content-mista">
          <div className="hero-image-mista"></div>
          <div className="hero-text-mista">
            <h2 className="hero-title-mista">Nossas atividades</h2>
            <p>
              Trabalhamos em 3 grandes pilares para promover transoformação real:
              fortalecendo laços, protegendo, o meio ambiente e cultivando o
              desenvolvimento, humano integral.
            </p>
            <div className="hero-actions-mista">
              <button className="btn btn-secondary-mista" onClick={scrollToActivities}>
                <SetaIcon/>conheça nosso trabalho
              </button>
            </div>
          </div>
        </div>
      </Overlay>

      {/* SEÇÃO DE PILARES: Mostra os 3 pilares de ação */}
      <section className="pillars-section">
        <div className="pillars-section-inner max-w-7xl mx-auto">
          <h2 className="pillars-title">Nossos Pilares de Ação</h2>
          <div className="pillars-grid">
            {Pillars.map((pillar, index) => (
              <div key={index} className="pillar-item">
                <div className="pillar-icon-wrapper">
                  {pillar.icon}
                </div>
                <h3 className="pillar-title">{pillar.title}</h3>
                <p className="pillar-description">{pillar.description}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* SEÇÃO DE LISTAGEM DE ATIVIDADES: Mostra os cards de atividades */}
      <section id="activities-list" className="activities-list-section">
        <div className="max-w-7xl mx-auto">
          <div className="activities-header">
            <h2 className="activities-main-title">
              Últimas Publicações
            </h2>
            {/* Botão para adicionar postagem (placeholder) */}
            <button
              onClick={handleAddActivity}
              className="add-post-button"
            >
              <IconPlus style={{ width: '1.25rem', height: '1.25rem' }} className="mr-2" />
              Adicionar Postagem
            </button>
          </div>

          {/* Grade de cards */}
          <div className="activities-grid">
            {activities.length > 0 ? (
              activities.map((activity) => (
                <ActivityCard key={activity.id} activity={activity} />
              ))
            ) : (
              <div className="pillar-item" style={{ gridColumn: '1 / -1' }}>
                <p style={{ fontSize: '1.25rem', color: 'var(--color-gray-500)' }}>Nenhuma atividade encontrada. Comece a cadastrar!</p>
              </div>
            )}
          </div>
        </div>
      </section>

      {/* MODAL: Placeholder para futura funcionalidade CRUD */}
      {showModal && (
        <div className="modal-backdrop">
          <div className="modal-content">
            <h3 className="modal-title">Em Desenvolvimento</h3>
            <p className="modal-body">
              A funcionalidade de CRUD (Adicionar/Editar/Remover Postagens) será implementada em nossa próxima etapa, utilizando Firebase Firestore para o backend.
            </p>
            <button
              onClick={closeModal}
              className="modal-close-button"
            >
              Entendi
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default App;
