import { Routes } from '@angular/router';
import { FacturasComponent } from './components/facturas/facturas.component';
import { AppComponent } from './app.component';
import { FormularioFacturaComponent } from './components/formulario-factura/formulario-factura.component';

export const routes: Routes = [
  {path: 'facturas', component: FacturasComponent},
  {path: 'formFactura', component: FormularioFacturaComponent},
  { path: '', redirectTo: '/', pathMatch: 'full' }
];
