import { Component } from "@angular/core";
import {
  LucideActivity,
  LucideBot,
  LucideBuilding2,
  LucideDynamicIcon,
  LucideFileText,
  LucideHome,
  LucideSettings,
  LucideUsersRound,
  type LucideIcon,
} from "@lucide/angular";

import { de } from "./core/i18n/de";

interface NavigationItem {
  label: string;
  icon: LucideIcon;
  active?: boolean;
}

@Component({
  selector: "eo-root",
  imports: [LucideActivity, LucideBot, LucideBuilding2, LucideDynamicIcon],
  templateUrl: "./app.component.html",
})
export class AppComponent {
  protected readonly copy = de;

  protected readonly navigation: NavigationItem[] = [
    { label: de.overview, icon: LucideHome, active: true },
    { label: de.properties, icon: LucideBuilding2 },
    { label: de.residents, icon: LucideUsersRound },
    { label: de.documents, icon: LucideFileText },
    { label: de.settings, icon: LucideSettings },
  ];
}
