using UnityEngine;

public class Tool : MonoBehaviour
{
    public int currentOption;
    public Placeable current;
    public Player player;
    public Placeable[] placeablePrefabs;
    public void Use(Vector2 worldPos)
    {
        
        var target = Physics2D.OverlapPoint(worldPos,LayerMask.GetMask("Placeable"));
        Placeable placeable = null;
        if (target)
        {
            placeable = target.GetComponentInParent<Placeable>();
        }
        switch (currentOption)
        {
            case 0:
            {
                if (!current && placeable)
                {
                    current = placeable;
                    
                    placeable.placed = false;
                    placeable.rotated = false;
                    current.StartMove();
                }
                else
                {
                    if (current)
                    {
                        if (!current.placed)
                        {
                            current.placed = true;
                            current.placedPos = worldPos;
                        }
                        else
                        {
                            current.rotated = true;
                            current.placedRot = current.transform.rotation;
                            current.StopMove();
                            current = null;
                        }
                    }
                   
                }

                break;
            }
            case 1:
            {
                if (placeable)
                {
                    Destroy(placeable.gameObject);
                    player.placed.Remove(placeable);
                }

                break;
            }
            case 2:
            case 3:
            case 4:
            {
                if (CanPlace())
                {
                    if (!current.placed)
                    {
                        current.placed = true;
                        current.placedPos = worldPos;
                    }
                    else
                    {
                        current.rotated = true;
                        current.placedRot = current.transform.rotation;
                        player.placed.Add(current);
                        current.StopMove();
                        current = Instantiate(current, worldPos, current.transform.rotation);
                        current.placed = false;
                        current.rotated = false;
                    }
                }
                break;
            }
            default: break;
        }
    }

    public void Update()
    {
        if(current )
        {
            var worldPos = player.cursorWorldPos;
            if (!current.placed)
            {
                current.transform.position = worldPos;
            }
            else
            {
                if (!current.rotated)
                {
                    var dir =  worldPos - current.placedPos;
                    if (dir.magnitude > 1f)
                    {
                        current.transform.up = dir.normalized;
                    }
                    else
                    {
                        current.transform.up = current.placedRot * Vector2.up;
                    }
                }
            }
           
    
        }
    }

    public void ChangeOption(int option)
    {
        if (current )
        {
            if (currentOption >= 2)
            {
                Destroy(current.gameObject);
            }

            if (currentOption == 0)
            {
                current.Reset();
            }
        }
        currentOption = option;
        if (option <= 1)
        {
            current = null;
            return;
        }
        current = Instantiate(placeablePrefabs[option-2], player.cursorWorldPos, Quaternion.identity);
        current.StartMove();
        current.placed = false;
        current.rotated = false;
    }
    public bool CanPlace()
    {
        return current;
    }
}