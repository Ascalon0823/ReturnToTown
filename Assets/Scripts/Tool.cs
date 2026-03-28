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
                }
                else
                {
                    if (current)
                    {
                        current.Place();
                        current = null;
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
                    current.Place();
                    player.placed.Add(current);
                    current = Instantiate(current, worldPos, current.transform.rotation);
                    current.placed = false;
                }
                break;
            }
            default: break;
        }
    }

    public void Update()
    {
        if(current && !current.placed)
        {
            var worldPos = player.cursorWorldPos;
            current.transform.position = worldPos;
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
        current.placed = false;
    }
    public bool CanPlace()
    {
        return current;
    }
}