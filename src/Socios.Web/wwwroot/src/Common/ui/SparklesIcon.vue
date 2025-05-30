<template>
    <svg
      xmlns="http://www.w3.org/2000/svg"
      id="sparkles-icon"
      viewBox="0 0 24 24"
      :width="currentSize"
      :height="currentSize"
      :fill="componentColor"
      :style="styleObject"
      aria-hidden="true" 
      role="img"
    >
      <path d="M10,21.236,6.755,14.745.264,11.5,6.755,8.255,10,1.764l3.245,6.491L19.736,11.5l-6.491,3.245ZM18,21l1.5,3L21,21l3-1.5L21,18l-1.5-3L18,18l-3,1.5ZM19.333,4.667,20.5,7l1.167-2.333L24,3.5,21.667,2.333,20.5,0,19.333,2.333,17,3.5Z"/>
    </svg>
  </template>
  
  <script>
  export default {
    name: 'SparklesIcon',
    props: {
      // Color base del icono cuando no está animado o como punto de partida.
      // La animación anulará 'fill' directamente.
      color: {
        type: String,
        default: 'currentColor' 
      },
      // Tamaño del icono, ej: '3rem', '48px'
      size: {
        type: String,
        default: '3.0rem' 
      },
      // Prop para activar/desactivar la animación
      animated: {
        type: Boolean,
        default: false 
      }
    },
    computed: {
      componentColor() {
        // Si está animado, la animación controla el 'fill'.
        // Si no está animado, usa el color provisto o currentColor.
        // Para asegurar que la animación comience desde un color definido por CSS si es necesario,
        // podríamos no establecer 'fill' aquí si está animado, o dejar que el keyframe 0% lo establezca.
        // Por simplicidad, este 'color' prop puede ser el color base.
        return this.color; 
      },
      currentSize() {
        return this.size;
      },
      styleObject() {
        const styles = {
          display: 'inline-block', // Comportamiento estándar para iconos
          verticalAlign: 'middle', // Ayuda a alinear mejor con texto adyacente
          lineHeight: 1
        };
        if (this.animated) {
          // La animación se define en CSS y se aplica aquí
          styles.animation = 'aiIconAnimate 2.5s infinite ease-in-out';
        }
        return styles;
      }
    }
  };
  </script>
  
  <style scoped>
  /* La animación de pulso y cambio de color para el SVG */
  @keyframes aiIconAnimate {
    0% {
      transform: scale(0.9);
      opacity: 0.7;
      fill: #FFD700; /* Gold - Color inicial */
      filter: drop-shadow(0 0 4px rgba(255, 215, 0, 0.6)); /* Sombra/brillo dorado */
    }
    25% {
      transform: scale(1.15);
      opacity: 1;
      fill: #00E5FF; /* Aqua/Cyan más vibrante */
      filter: drop-shadow(0 0 7px rgba(0, 229, 255, 0.7));
    }
    50% {
      transform: scale(0.95);
      opacity: 0.8;
      fill: #FF00B2; /* Fucsia/Magenta más vibrante */
      filter: drop-shadow(0 0 5px rgba(255, 0, 178, 0.6));
    }
    75% {
      transform: scale(1.1);
      opacity: 1;
      fill: #BF55EC; /* Lila/Púrpura medio */
      filter: drop-shadow(0 0 7px rgba(191, 85, 236, 0.7));
    }
    100% {
      transform: scale(0.9);
      opacity: 0.7;
      fill: #FFD700; /* Gold - De vuelta al color inicial */
      filter: drop-shadow(0 0 4px rgba(255, 215, 0, 0.6));
    }
  }
  </style>