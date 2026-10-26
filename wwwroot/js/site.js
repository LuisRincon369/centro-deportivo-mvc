// =============================================================
// Centro de Entrenamiento Deportivo - JavaScript del navegador
// =============================================================

// ---------- Modal de confirmación para eliminar un inscrito ----------
// Bootstrap dispara el evento "show.bs.modal" justo antes de abrir el modal.
// Leemos los data-* del botón que lo abrió y llenamos el formulario POST.
const modalEliminar = document.getElementById("modalEliminar");

if (modalEliminar) {
    modalEliminar.addEventListener("show.bs.modal", (evento) => {
        const boton = evento.relatedTarget;
        document.getElementById("idEliminar").value = boton.dataset.id;
        document.getElementById("nombreEliminar").textContent = boton.dataset.nombre;
    });
}
