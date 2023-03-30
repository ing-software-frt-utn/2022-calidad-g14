using ControlDeCalidad.Dominio.EntidadesBase;
using ControlDeCalidad.Dominio.Enumeraciones;

namespace ControlDeCalidad.Dominio.Entidades
{
    public class Semaforo : EntidadPersistible
    {
        public Semaforo() { }

        public Semaforo(int limiteInferior, int limiteSuperior, TipoDeDefecto tipo)
        {
            Tipo = tipo;
            LimiteInferior = limiteInferior;
            LimiteSuperior = limiteSuperior;
            Contador = 0;
            EstablecerColor();
        }

        //private Modelo _modelo;
        public TipoDeDefecto Tipo { get; set; }
        public int LimiteInferior { get; set; }
        public int LimiteSuperior { get; set; }
        public int Contador { get; set; }
        public ColorSemaforo Color { get; set; }

        public event EventHandler<TipoDeDefecto> LimiteInferiorEvent;
        public event EventHandler<TipoDeDefecto> LimiteSuperiorEvent;

        public void ReiniciarContador()
        {
            Contador = 0;
        }

        public void ActualizarContador(short valor)
        {
            Contador += valor;
            VerificarLimite();            
        }

        private void VerificarLimite()
        {
            if (Contador == LimiteInferior)
            {
                EstablecerColor();
                LimiteInferiorEvent?.Invoke(this, this.Tipo);
            }
            else if (Contador == LimiteSuperior)
            {
                EstablecerColor();
                LimiteSuperiorEvent?.Invoke(this, this.Tipo);
            }
        }

        private void EstablecerColor()
        {
            if (Contador < LimiteInferior)
                Color = ColorSemaforo.Verde;
            else if (Contador >= LimiteInferior && Contador < LimiteSuperior)
                Color = ColorSemaforo.Amarillo;
            else if (Contador >= LimiteSuperior)
                Color = ColorSemaforo.Rojo;
        }

        public void VerificarEstado()
        {
            // Se podria usar un lambda para cambiar el == por >=
            VerificarLimite();
        }
    }
}
