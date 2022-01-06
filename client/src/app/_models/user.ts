import { CongregationRole } from "./congregationRole";

export interface User {
  username: string;
  firstName: string;
  surname: string;
  created: Date;
  lastActive: Date;
  congregationRoles: CongregationRole[];
  congregationId: number;
  admin: boolean;
  token: string;
}