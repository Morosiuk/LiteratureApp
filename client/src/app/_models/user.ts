import { CongregationRole } from "./congregationRole";

export interface User {
  username: string;
  firstName: string;
  surname: string;
  congregationRoles: CongregationRole[];
  token: string;
}